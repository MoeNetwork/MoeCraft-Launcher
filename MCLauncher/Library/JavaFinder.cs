using System;
using System.IO;
using Microsoft.Win32;

namespace MCLauncher.Library
{
    public sealed class JavaFinder
    {
        private string _targetVersion;

        public JavaFinder(string targetVersion)
        {
            _targetVersion = targetVersion;
        }

        public JavaFindResult FindJava()
        {
            JavaFindResult localIntegratedResult = null;
            JavaFindResult registryResult = null;

            foreach (var searchMode in ApplicationEnvironment.SearchModeOrder)
            {
                if ((searchMode == SearchMode.FromIntegrated || searchMode == SearchMode.FromIntegratedCompat) && localIntegratedResult == null)
                {
                    localIntegratedResult = FindFromLocalIntegrated();

                    if (localIntegratedResult.IsSuccess && !localIntegratedResult.IsCompat)
                        return localIntegratedResult;
                }

                if ((searchMode == SearchMode.FromRegistry || searchMode == SearchMode.FromRegistryCompat) && registryResult == null)
                {
                    registryResult = FindFormRegistry();

                    if (registryResult.IsSuccess && !registryResult.IsCompat)
                        return registryResult;
                }
            }

            foreach (var searchMode in ApplicationEnvironment.SearchModeOrder)
            {
                if (searchMode == SearchMode.FromRegistryCompat && registryResult != null)
                {
                    return registryResult;
                } 
                
                if (searchMode == SearchMode.FromIntegratedCompat && localIntegratedResult != null)
                {
                    return localIntegratedResult;
                }
            }

            return makeFault();
        }

        private string GetPlatformDirectoryName()
        {
            return Environment.Is64BitOperatingSystem
                ? ApplicationEnvironment.X64DirectoryName
                : ApplicationEnvironment.X86DirectoryName;
        }

        private string SearchJavaDirectory(string platformPath)
        {
            string directorySearchResult = null;

            foreach (var directoryName in ApplicationEnvironment.LocalJavaDirectoryNameSearchOrder)
            {
                if (Directory.Exists(Path.Combine(directoryName, platformPath)))
                {
                    directorySearchResult = directoryName;
                    break;
                }
            }

            return directorySearchResult;
        }

        private JavaFindResult FindFromLocalIntegrated()
        {
            string directorySearchResult = SearchJavaDirectory(GetPlatformDirectoryName());
            bool isCompat = false;

            if (directorySearchResult == null && Environment.Is64BitOperatingSystem)
            {
                directorySearchResult = SearchJavaDirectory(ApplicationEnvironment.X86DirectoryName);
                if (directorySearchResult != null)
                    isCompat = true;
            }
                
            return new JavaFindResult(directorySearchResult == null, isCompat, directorySearchResult);
        }

        private JavaFindResult makeFault()
        {
            return new JavaFindResult(false, false, null);
        }

        private JavaFindResult FindFormRegistryFromBaseKey(RegistryKey baseKey, bool isCompat = false)
        {
            if (baseKey == null)
                return makeFault();

            var registryNode = baseKey.OpenSubKey(ApplicationEnvironment.TargetJavaVersion);

            if (registryNode == null)
                return makeFault();

            var registryJavaHomeValue = registryNode.GetValue("JavaHome");

            if (registryJavaHomeValue == null)
                return makeFault();

            return new JavaFindResult(true, isCompat, registryJavaHomeValue.ToString());
        }

        private JavaFindResult FindFormRegistry()
        {
            JavaFindResult result = FindFormRegistryFromBaseKey(ApplicationEnvironment.JavaRegistryNode);

            if (!result.IsSuccess && Environment.Is64BitOperatingSystem)
            {
                return FindFormRegistryFromBaseKey(ApplicationEnvironment.JavaRegistryNodeCompat, true);
            }

            return result;
        }
    }
}

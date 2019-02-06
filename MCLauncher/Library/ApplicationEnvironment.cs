using Microsoft.Win32;

namespace MCLauncher.Library
{
    class ApplicationEnvironment
    {
        public const string TargetJavaVersion = "1.8";

        public static readonly SearchMode[] SearchModeOrder =
        {
            SearchMode.FromIntegrated, SearchMode.FromRegistry, SearchMode.FromRegistryCompat,
            SearchMode.FromIntegratedCompat
        };

        public static readonly string[] SearchFileOrder = 
        {
            "MoeCraft-Toolbox.jar",
            "MoeCraft-Updater.jar",
            "Updater.jar",
            "更新器.jar",
            @"MoeCraft\Launcher.jar",
            @"MoeCraft\启动器.jar",
            "Launcher.jar",
            "启动器.jar"
        };

        public static readonly RegistryKey JavaRegistryNode =
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");

        public static readonly RegistryKey JavaRegistryNodeCompat =
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\JavaSoft\Java Runtime Environment");

        public static readonly string[] LocalJavaDirectoryNameSearchOrder =
        {
            "Java",
            @"..\Java",
            @"MoeCraft\Java",
            @"Updater\Java",
            @"..\Updater\Java",
            @"MoeCraft\Updater\Java",
        };

        public const string X64DirectoryName = "x64";
        public const string X86DirectoryName = "x86";

        public const string JavaDownloadURL =
            "http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html";

        public const string JavaVersionReadable = "Java 8";
        public const bool DisallowJava9 = true;
    }
}

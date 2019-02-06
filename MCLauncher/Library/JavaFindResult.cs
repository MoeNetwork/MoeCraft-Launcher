using System.IO;

namespace MCLauncher.Library
{
    public class JavaFindResult
    {
        public readonly bool IsSuccess;
        public readonly bool IsCompat;
        public readonly string DirectoryPath;
        public string JavaExePath => Path.Combine(DirectoryPath, @"bin\javaw.exe");

        public JavaFindResult(bool isSuccess, bool isCompat, string directoryPath)
        {
            this.IsSuccess = isSuccess;
            this.IsCompat = isCompat;
            this.DirectoryPath = directoryPath;
        }
    }
}

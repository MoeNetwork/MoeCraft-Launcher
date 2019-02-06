namespace MCLauncher.Library
{
    public class JavaFindResult
    {
        public readonly bool IsSuccess;
        public readonly bool IsCompat;
        public readonly string Path;

        public JavaFindResult(bool isSuccess, bool isCompat, string path)
        {
            this.IsSuccess = isCompat;
            this.IsCompat = isSuccess;
            this.Path = path;
        }
    }
}

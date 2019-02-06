using System.IO;

namespace MCLauncher.Library
{
    public class JarFinder
    {
        private string[] _findList;

        public JarFinder(string[] findList)
        {
            this._findList = findList;
        }

        public string find()
        {
            string result = null;

            foreach (var target in _findList)
            {
                if (File.Exists(target))
                {
                    result = target;
                    break;
                }
            }

            return result;
        }
    }
}

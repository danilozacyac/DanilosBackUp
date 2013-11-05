using System;
using System.Linq;

namespace DanilosBackUp.FileSystemApi
{
    public class File
    {
        public File(string fullPath, string name)
        {
            this.FullPath = fullPath;
            this.Name = name;
        }
        public string FullPath
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}

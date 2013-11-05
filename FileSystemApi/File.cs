using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSystemApi
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

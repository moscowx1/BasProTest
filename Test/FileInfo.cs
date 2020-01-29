using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class FileInfo
    {
        public bool IsValid;
        public string[] Paths;
        public int? Size;
        public string Name;
        public FileInfo(string path)
        {
            string[] fileInfo = path.Split();
            string[] paths = fileInfo[0].Split('\\');
            if (paths.Any(x => string.IsNullOrEmpty(x)))
                return;
            Paths = paths.Take(paths.Length - 1).ToArray();
            Name = paths.Last();
            if (fileInfo.Length == 2)
            {
                if (int.TryParse(fileInfo[1], out int size))
                    Size = size;
                else
                    return;
            }
            IsValid = true;
        }
    }
}

using System.Collections.Generic;

namespace Test
{
    class File
    {
        public int? Size;
        public string Name;
        public File(string name, int? size)
        {
            Name = name;
            Size = size ?? 0;
        }
    }
    class Folder : File
    {
        public Folder(string name):base(name, null) { }

        public List<Folder> Folders = new List<Folder>();

        public List<File> Files = new List<File>();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test
{
    class Program
    {
        static void GetPaths(string path)
        {
            if (!System.IO.File.Exists(path))
                return;
            using StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                line = Regex.Replace(line, @"\s+", " ").Trim();
                if (line == "")
                    continue;
                FileInfo info = new FileInfo(line);
                if (!info.IsValid)
                    continue;
                AddFile(info);
            }
            sr.Close();
        }
        static void AddFile(FileInfo info)
        {
            Folder folder = mainFolder;
            for(int i = 0; i < info.Paths.Length; i++)
            {
                string s = info.Paths[i];
                Folder temp = folder.Folders.Find(x => x.Name == s);
                if (temp != null)
                    folder = temp;
                else
                {
                    folder.Folders.Add(new Folder(s));
                    folder = folder.Folders.Last();
                }       
            }

            if (info.Name.ToUpper() != info.Name)
                folder.Files.Add(new File(info.Name, info.Size));
            else
                folder.Folders.Add(new Folder(info.Name));
        }

        private static Folder mainFolder = new Folder("Test");
        static void PrintFolder(Folder folder, int indent = 0)
        {
            void print(string s)
            {
                string ind = new string('-', indent);
                Console.WriteLine($"|{ind}{s}");
            }
            List<Folder> folders = folder.Folders
                .OrderBy(f => f.Name).ToList();
            foreach(Folder f in folders)
            {
                print(f.Name);
                PrintFolder(f, indent + 1);
            }
            List<File> files = folder.Files
                .OrderByDescending(f => f.Size)
                .ThenBy(f => f.Name).ToList();
            foreach (File f in files)
                print(f.Name);
        }
        static void Main(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string inputName = "input.txt";
            GetPaths(Path.Combine(currentPath, inputName));
            PrintFolder(mainFolder);
        }
    }
}

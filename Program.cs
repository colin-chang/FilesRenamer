using System;
using System.IO;

namespace Renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = string.Empty;
            while (!Directory.Exists(dir))
            {
                var msg = string.IsNullOrWhiteSpace(dir)
                    ? "Please enter the root directory:"
                    : "root directory doesn't exist,try to enter a new one:";
                Console.WriteLine(msg);
                dir = Console.ReadLine();
            }

            var files = Directory.GetFiles(dir, String.Empty, searchOption: SearchOption.AllDirectories);
            if (files.Length <= 0)
                Console.WriteLine("Done. there is no file.");

            Console.WriteLine("Replace from ?");
            var from = Console.ReadLine();
            Console.WriteLine("Replace to ?");
            var to = Console.ReadLine();

            var cnt = 0;
            try
            {
                foreach (var file in files)
                {
                    //skip hidden file on linux/mac
                    if (file.StartsWith("."))
                        continue;

                    var newName = Path.Combine(Path.GetDirectoryName(file),
                        Path.GetFileName(file).Replace(from, to));
                    new FileInfo(file).MoveTo(newName);
                    
                    Console.WriteLine($"Rename {file} to {newName}");
                    cnt++;
                }

                Console.WriteLine($"Done.Renamed {cnt} files.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Done with error.Renamed {cnt} files.\r\nError:{e.Message}");
            }
        }
    }
}
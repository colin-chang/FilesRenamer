using System;
using System.IO;

namespace ColinChang.Renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error.invalid arguments...");
                return;
            }

            var dir = args[0];
            if (!Directory.Exists(dir))
            {
                Console.WriteLine($"Error.{args[0]} does not exist...");
                return;
            }

            var opt = string.Equals(args[1], "-y") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.GetFiles(dir, string.Empty, searchOption: opt);
            if (files.Length <= 0)
            {
                Console.WriteLine($"Done.there are no files under {args[0]}.");
                return;
            }

            var from = args.Length < 3 ? string.Empty : args[2];
            if (string.IsNullOrWhiteSpace(from))
            {
                Console.WriteLine("Done.replace pattern is empty...");
                return;
            }

            var to = args.Length < 4 ? string.Empty : args[3];

            var cnt = 0;
            try
            {
                foreach (var file in files)
                {
                    //skip hidden file on linux/mac
                    if (Path.GetFileName(file).StartsWith("."))
                        continue;

                    var newName = Path.Combine(Path.GetDirectoryName(file),
                        Path.GetFileName(file).Replace(from, to));
                    if (string.Equals(file, newName))
                        continue;

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
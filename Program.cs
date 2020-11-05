using System;
using System.IO;
using DirCopying;

namespace CustomThreadPulling
{
    class Program
    {
        static void Main(string[] args)
        {
            // Directory copying.
            Console.Write("Enter source directory: ");
            var srcDir = Console.ReadLine();
            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine("Directory does not exist");
                return;
            }

            Console.Write("Enter destination directory: ");
            var destDir = Console.ReadLine();
            if (!Directory.Exists(destDir))
            {
                Console.WriteLine("Directory does not exist");
                return;
            }

            DirectoryCopier.ParallelQueueDirCopy(srcDir, destDir);
            Console.WriteLine($"Copied files: {DirectoryCopier.CopiedFiles}");
            Console.WriteLine("Processing complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DirCopying;

namespace CustomThreadPulling
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskQueue = new TaskQueue(10);
            TaskDelegate task = WriteSomeText;
            for (var i = 1; i <= 10; i++)
            {
                taskQueue.EnqueueTask(task);
            }
            Task.WaitAll();
            Thread.Sleep(1000);
            Console.WriteLine();
            
            // Directory copying.
            Console.Write("Enter source directory: ");
            var srcDir = Console.ReadLine();
            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine("Directory does not exists");
                return;
            }

            Console.Write("Enter destination directory: ");
            var destDir = Console.ReadLine();
            if (!Directory.Exists(destDir))
            {
                Console.WriteLine("Directory does not exists");
                return;
            }

            DirectoryCopier.ParallelDirCopy(srcDir, destDir);
            Console.WriteLine($"Copied files: {DirectoryCopier.CopiedFiles}");
            Console.WriteLine("Processing complete. Press any key to exit.");
            Console.ReadKey();

        }

        static void GetDirectories(string dir)
        {
            var srcDirs = Directory.GetDirectories(dir);
            foreach (string subdir in srcDirs)
            {
                Console.WriteLine(subdir);
                if (Directory.GetDirectories(subdir) != null)
                {
                    GetDirectories(subdir);
                }
                GetFiles(subdir);
            }
        }

        static void GetFiles(string dir)
        {
            var filesList = Directory.GetFiles(dir);
            foreach (string file in filesList)          
                Console.WriteLine(Path.GetFileName(file));
        }

        static void WriteSomeText()
        {
            Console.WriteLine("This text is written from " 
                + Thread.CurrentThread.ManagedThreadId + " thread");
        }
    }
}
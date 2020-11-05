using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CustomThreadPulling;

namespace DirCopying
{
    public static class DirectoryCopier
    {
        private static int copiedFiles = 0;
        public static int CopiedFiles
        {
            get => copiedFiles;
        }

        // Параллельное копирование файлов при помощи класса TaskQueue.
        public static void ParallelQueueDirCopy(string sDir, string dDir)
        {
            if (Directory.Exists(sDir))
            {
                foreach (string dir in Directory.GetDirectories(sDir))
                {
                    var destDirName = new DirectoryInfo(dir);
                    string newDestDir = Path.Combine(dDir, destDirName.Name);
                    ParallelQueueDirCopy(dir, newDestDir);
                }

                if (!Directory.Exists(dDir))
                    Directory.CreateDirectory(dDir);

                var taskQueue = new TaskQueue(6);
                foreach (string file in Directory.GetFiles(sDir))
                {
                    TaskDelegate copyingTask = () =>
                    {
                        string fileName = Path.GetFileName(file);
                        string sourceFile = Path.Combine(sDir, fileName);
                        string destFile = Path.Combine(dDir, fileName);
                        File.Copy(sourceFile, destFile, true);
                        Interlocked.Increment(ref copiedFiles);
                        Console.WriteLine($"COPIED by {Thread.CurrentThread.ManagedThreadId} thread- "
                            + destFile);
                    };
                    taskQueue.EnqueueTask(copyingTask);
                }
                Task.WaitAll(taskQueue.activeTaskList.ToArray());
            }
        }

        // Параллельное копирование файлов при помощи класса Parallel.
        public static void ParallelDirCopy(string sDir, string dDir)
        {
            if (Directory.Exists(sDir))
            {
                foreach (string dir in Directory.GetDirectories(sDir))
                {
                    var destDirName = new DirectoryInfo(dir);
                    string newDestDir = Path.Combine(dDir, destDirName.Name);
                    ParallelDirCopy(dir, newDestDir);
                }

                if (!Directory.Exists(dDir))
                    Directory.CreateDirectory(dDir);
                Parallel.ForEach(Directory.GetFiles(sDir), (currentFile) =>
                {
                    string fileName = Path.GetFileName(currentFile);
                    string sourceFile = Path.Combine(sDir, fileName);
                    string destFile = Path.Combine(dDir, fileName);
                    File.Copy(sourceFile, destFile, true);
                    Interlocked.Increment(ref copiedFiles);
                    Console.WriteLine($"COPIED by {Thread.CurrentThread.ManagedThreadId} thread- " 
                        + destFile);
                });
            }
        }

        // Последовательное копирование файлов в одном потоке.
        public static void DirCopy(string sDir, string dDir)
        {
            if (Directory.Exists(sDir))
            {
                foreach (string dir in Directory.GetDirectories(sDir))
                {
                    var destDirName = new DirectoryInfo(dir);
                    string newDestDir = Path.Combine(dDir, destDirName.Name);
                    DirCopy(dir, newDestDir);
                }

                if (!Directory.Exists(dDir))
                    Directory.CreateDirectory(dDir);
                foreach (string file in Directory.GetFiles(sDir))
                {
                    string fileName = Path.GetFileName(file);
                    string sourceFile = Path.Combine(sDir, fileName);
                    string destFile = Path.Combine(dDir, fileName);
                    File.Copy(sourceFile, destFile, true);
                    copiedFiles++;
                    Console.WriteLine("COPIED - " + destFile);
                }
            }
        }
    }
}
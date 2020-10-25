using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DirCopying
{
    public static class DirectoryCopier
    {
        private static int copiedFiles = 0;
        public static int CopiedFiles
        {
            get => copiedFiles;
        }
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
                    Interlocked.Increment(ref copiedFiles);
                    Console.WriteLine("COPIED - " + destFile);
                }
            }
        }
    }
}
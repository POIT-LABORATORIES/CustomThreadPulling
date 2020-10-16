using System;
using System.Threading;

namespace CustomThreadPulling
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskQueue = new TaskQueue(12);
            int CountofWorkThreads;
            int CountofImputOutputThreads;
            ThreadPool.GetMinThreads(out CountofWorkThreads, out CountofImputOutputThreads);
            Console.WriteLine("Work threads number: " + CountofWorkThreads +
                "\nIO threads number: " + CountofImputOutputThreads);
        }
    }
}
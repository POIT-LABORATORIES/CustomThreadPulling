using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomThreadPulling
{
    class Program
    {
        static void Main(string[] args)
        {
           var taskQueue = new TaskQueue(10);
           TaskDelegate task = WriteSomeText;
           for (var i = 1; i <= 15; i++)
           {
               taskQueue.EnqueueTask(task);
           }
           Task.WaitAll();
        }

        static void WriteSomeText()
        {
            Console.WriteLine("This text is written from " + Thread.CurrentThread.ManagedThreadId + " thread");
        }
    }
}
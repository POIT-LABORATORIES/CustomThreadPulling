using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CustomThreadPulling
{
    public delegate void TaskDelegate();
    public class TaskQueue
    {
        private int attempt = 0;
        private ConcurrentQueue<TaskDelegate> taskQueue = new ConcurrentQueue<TaskDelegate>();

        // Создание указанного количества потоков пула.
        public TaskQueue(int threadQuantity)
        {
            ThreadPool.SetMaxThreads(threadQuantity, threadQuantity);
        }

        public void EnqueueTask(TaskDelegate task)
        {
            taskQueue.Enqueue(task);
            ExecuteTask();
        }

        private void ExecuteTask()
        {
            while (taskQueue.Count != 0)
            {
                TaskDelegate task;
                if (!taskQueue.TryDequeue(out task)) 
                    continue;
                attempt++;
                Thread.Sleep(10);
                Console.WriteLine(attempt + " attempt:");
                Task.Run(() => task());
            }
        }
    }
}
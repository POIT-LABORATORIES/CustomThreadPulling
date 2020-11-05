using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomThreadPulling
{
    public delegate void TaskDelegate();
    public class TaskQueue
    {
        private ConcurrentQueue<TaskDelegate> taskQueue = new ConcurrentQueue<TaskDelegate>();
        public readonly List<Task> activeTaskList = new List<Task>();

        // Создание указанного количества потоков пула.
        public TaskQueue(int threadQuantity)
        {
            ThreadPool.SetMinThreads(threadQuantity, threadQuantity);
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
                if (!taskQueue.TryDequeue(out TaskDelegate task))
                    continue;
                activeTaskList.Add(Task.Run(() => task()));
            }
        }
    }
}
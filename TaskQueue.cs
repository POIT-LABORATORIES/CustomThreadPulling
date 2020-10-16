using System;
using System.Threading;

namespace CustomThreadPulling
{
    public class TaskQueue
    {
        //void delegate TaskDelegate();

        // Содержит очередь задач в виде делегатов без параметров.
        public TaskQueue(int threadQuantity)
        {
            // Создание указанного количества потоков пула.
            ThreadPool.SetMinThreads(threadQuantity, threadQuantity);
        }
    }

    /*Чтобы запросить поток из пула для обработки вызова метода, 
     * можно использовать метод QueueUserWorkItem(). Этот метод 
     * перегружен, чтобы в дополнение к экземпляру делегата 
     * WaitCallback позволить указывать необязательный параметр 
     * System.Object для специальных данных состояния.*/
}
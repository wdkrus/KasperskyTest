using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public class CustomQueue<T>
    {
        private Queue<T> container = new Queue<T>();
        private Queue<TaskCompletionSource<T>> popTaskCompletionsQueue = new Queue<TaskCompletionSource<T>>();

        private object locker = new object();

        public async Task<T> Pop()
        {
            TaskCompletionSource<T> popTaskCompletion = new TaskCompletionSource<T>();

            lock (locker)
            {
                popTaskCompletionsQueue.Enqueue(popTaskCompletion);
            }

            return await popTaskCompletion.Task;
        }

        public void Push(T item)
        {
            lock (locker)
            {
                container.Enqueue(item);

                if (popTaskCompletionsQueue.Count > 0)
                    popTaskCompletionsQueue.Dequeue().SetResult(container.Dequeue());
            }
        }
    }
}

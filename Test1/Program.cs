using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            CustomQueue<int> queue = new CustomQueue<int>();

            //Инициализируем очередь, первые пять pop-ов должны будут вернуть от 0 до 4
            for (int i = 0; i < 5; i++)
            {
                queue.Push(i);
            }

            //Эти потоки будут извлекать элеменеты из очереди, ожидая добавления новых
            for (int i = 0; i < 10; i++)
            {
                int number = i;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Task {0} pop", number);

                    Task<int> popTask = queue.Pop();
                    popTask.Wait();

                    Console.WriteLine("Task {0} pop succeded, value: {1}", number, popTask.Result);
                });

                //Небольшая задержка, чтобы сохранить хронологию начала ожидания
                Thread.Sleep(100);
            }

            //Эти потоки будут периодически добавлять новые элементы
            for (int i = 0; i < 10; i++)
            {
                int number = i;

                Task.Factory.StartNew(() =>
                {
                    int val = rnd.Next(10) + 1;
                    int sleepTime = rnd.Next(10000);

                    Thread.Sleep(sleepTime);

                    Console.WriteLine("Task {0} push {1}", number, val);
                    queue.Push(val);
                });
            }

            Console.ReadKey();
        }
    }
}
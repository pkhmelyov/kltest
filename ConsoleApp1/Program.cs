using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperQueueDemo();

            Console.WriteLine("Press any key...");
            Console.ReadKey(intercept: true);

            PairsExampleFromTaskDemo();

            Console.WriteLine("Press any key...");
            Console.ReadKey(intercept: true);

            PairsDemo();
        }

        #region Super Queue Demo

        static void GetItem(MySuperQueue<int> queue)
        {
            Console.WriteLine($"Thread \"{Thread.CurrentThread.Name}\": got {queue.Pop()}");
        }

        static void PutItem(MySuperQueue<int> queue, int item)
        {
            queue.Push(item);
            Console.WriteLine($"Thread \"{Thread.CurrentThread.Name}\": put {item}");
        }

        static void SuperQueueDemo()
        {
            var queue = new MySuperQueue<int>();

            var threads = new List<Thread>();

            var thread = new Thread(() => GetItem(queue));
            thread.Name = "Get #1";
            threads.Add(thread);

            thread = new Thread(() => GetItem(queue));
            thread.Name = "Get #2";
            threads.Add(thread);

            thread = new Thread(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                PutItem(queue, 1);
            });
            thread.Name = "Put #1";
            threads.Add(thread);

            thread = new Thread(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(8));
                PutItem(queue, 2);
            });
            thread.Name = "Put #2";
            threads.Add(thread);

            thread = new Thread(() => GetItem(queue));
            thread.Name = "Get #3";
            threads.Add(thread);

            foreach (var t in threads) t.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(10));
            queue.Push(3);

            foreach (var t in threads) t.Join();
        }

        #endregion

        #region Pairs Demo

        static int[] GetRandomNumbers(int count = 100000, int maxValue = 300)
        {
            Random r = new Random();
            var result = new int[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = r.Next(maxValue);
            }

            return result;
        }

        static void PairsExampleFromTaskDemo()
        {
            foreach (var pair in Pairs.GetPairs(new []{ 1, 1, 2, 1, 1, 0, 1 }, 2))
            {
                Console.WriteLine($"({string.Join(", ", pair)})");
            }
        }

        static void PairsDemo()
        {
            foreach (var pair in Pairs.GetPairs(GetRandomNumbers(), 150))
            {
                Console.WriteLine($"({string.Join(", ", pair)})");
            }
        }

        #endregion
    }
}

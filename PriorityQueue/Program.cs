using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    class Program
    {
        static void Test1()
        {
            Console.WriteLine("Performing Test #1...");
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Enqueue(5, 5);
            queue.Enqueue(9, 9);
            queue.Enqueue(2, 2);
            queue.Enqueue(7, 7);
            queue.Enqueue(1, 1);
            queue.Enqueue(3, 3);
            queue.Enqueue(8, 8);

            while (queue.isEmpty == false)
            {
                Console.WriteLine("Max: " + queue.PeakMax());
                Console.WriteLine("Elements: " + queue.ToString());
                Console.WriteLine("Min: " + queue.DequeueMin());
                Console.WriteLine(string.Empty);
            }
        }

        static void Test2()
        {
            Console.WriteLine("Performing Test #2...");
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Enqueue(1, 1);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 2);
            queue.Enqueue(4, 2);
            queue.Enqueue(5, 5);
            queue.Enqueue(6, 6);
            queue.Enqueue(7, 7);

            while (queue.isEmpty == false)
            {
                Console.WriteLine("Max: " + queue.PeakMax());
                Console.WriteLine("Elements: " + queue.ToString());
                Console.WriteLine("Min: " + queue.DequeueMin());
                Console.WriteLine(string.Empty);
            }
        }

        static void Main(string[] args)
        {
            Test1();
            Test2();
        }
    }
}

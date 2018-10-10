using System;
using System.Threading;
using System.Threading.Tasks;

namespace _70483._1._1
{
    public static class TPL
    {
        public static void ParallelInvoke()
        {
            //syncronous
            Console.WriteLine("Syncronous Processing");
            Task1();
            Task2();
            //now try it asyncronously
            Console.WriteLine("Asyncronous Processing");
            Parallel.Invoke( () => Task1(), () => Task2());
            Console.WriteLine("Completed.");

        }
        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }
        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }
    }
}
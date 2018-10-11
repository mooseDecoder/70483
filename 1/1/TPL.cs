using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _70483._1._1
{
    public static class TPL
    {
        public static void ParallelInvoke()
        {
            Console.WriteLine("Parallel.Invoke");
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

        public static void ParallelForEach()
        {
            var items = Enumerable.Range(0,500);
            //syncronous
            Console.WriteLine("Parallel.ForEach Syncronous");
            
            foreach(var item in items)
            {
                if (item % 100 == 0)
                {
                    Console.WriteLine("Now Completing: {0}", item);
                }
            }
            //async
            Console.WriteLine("Parallel.ForEach Async");
            Parallel.ForEach(items, item=>
            {
                WorkOnItem(item);
            });


        }
        static void WorkOnItem(object item)
        {   if((int)item % 1 == 0)
            {
                Console.WriteLine("Started working on: {0}", item);
                Thread.Sleep(100);
                Console.WriteLine("Ending working on: {0}", item);
            }
            else
            {
                Thread.Sleep(100);
            } 
        }

        public static void ParallelFor()
        {
            var items = Enumerable.Range(0, 500).ToArray();
            Console.WriteLine("Async ParallelFor");
            Parallel.For(0, items.Length, i=>
            {
                WorkOnItem(items[i]);
            });

        }
        public static void ParallelLoopState()
        {
            var items = Enumerable.Range(0, 50).ToArray();
            Console.WriteLine("Async ParallelLoopState - with stop");
            int counter = 0;
            ParallelLoopResult result = Parallel.For(0, items.Length, 
                (int i, ParallelLoopState loopState)=>
                {
                    if(i == 25)
                    {
                        loopState.Stop();
                    }
                    WorkOnItem(items[i]);
                    counter++;
                });
                Console.WriteLine("Number of items: {0}", counter);
        }
    }
}
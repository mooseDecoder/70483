using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _70483.SupplementalClasses;

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

       public static void AsParallel()
       {

           Person [] people = new Person [1000000];
           
            for(int i = 0; i<people.Length; i++)
            {
                //Console.WriteLine("Starting with i: {0}", i);
                people[i] = new Person
                {
                    Name=Utilities.RandomString(5),
                    City= Utilities.RandomCity()
                };
                //Console.WriteLine("Name: {0} City {1}",people[i].Name, people[i].City );
                
            }
            Array.Sort(people, delegate(Person x, Person y) { return x.Name.CompareTo(y.Name); });

            int count = 0;
           Console.WriteLine("Syncronous selection");
           var stopwatch = new Stopwatch();
           
           var result = from person in people
                        where person.City == "Stuttgart"
                        select person;
            stopwatch.Start();
            foreach (var p in result)
            {
                count++;
                //Console.WriteLine("Selected Name: {0}, City {1}", p.Name, p.City);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0} for {1} items.", stopwatch.Elapsed, count);

            // Console.WriteLine("Now async selection with AsParallel");
            // stopwatch = new Stopwatch();
            // stopwatch.Start();
            // result = from person in people.AsParallel()
            //             where person.City == "Miami"
            //             select person.Name;
            // stopwatch.Stop();
            // foreach (var name in result)
            // {
            //     Console.WriteLine(name);
            // }
            // Console.WriteLine("Elapsed: {0}", stopwatch.ElapsedTicks);

            count = 0;
            Console.WriteLine("Now async selection with AsParallel + other options");
            stopwatch = new Stopwatch();
            

            result = from person in people.AsParallel()
                        //.WithDegreeOfParallelism(8)
                        where person.City == "Stuttgart"
                        select person;
            stopwatch.Start();
            // result.ForAll(p=>{
            //      count++;
            // });
            foreach (var p in result)
            {
                count++;
                //Console.WriteLine("Selected Name: {0}, City {1}", p.Name, p.City);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0} for {1} items.", stopwatch.Elapsed, count);


       } 
       public static void Tasks()
       {
           Task<int> t = Task.Run(()=>{

               return CalculateResult();
           });

            Console.WriteLine(t.Result);
            Console.WriteLine("Finished.");
       }
       static int CalculateResult()
       {
           Console.WriteLine("Work starting");
           Thread.Sleep(2000);
           Console.WriteLine("Work Completed.");
           return 99;
       }

    }
}
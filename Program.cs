using System;
using _70483._1._1;

namespace _70483
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! From Main");
            RunSection1_1();

        }
        static void RunSection1_1()
        {
            //TPL.ParallelInvoke();
            //TPL.ParallelForEach();
            //TPL.ParallelFor();
            //TPL.ParallelLoopState();
            //TPL.AsParallel();
            //TPL.Tasks();
            //TPL.Tasks2();
            //TPL.WaitAll();
            //TPL.Continuation();
            //TPL.Threads();
            TPL.ThreadLocal();
        }
        
    }
}

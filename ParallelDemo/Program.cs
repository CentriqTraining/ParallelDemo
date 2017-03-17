using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Create a list of number between 1 and 400,000
            var nums = Enumerable.Range(1, 400000);

            //  we are going to time each operation to see differences
            var sw = new Stopwatch();
            
            // start your engines!
            sw.Start();


            List<int> primes = new List<int>();
            
            //  Prime is defined as a number that can only be
            //  Evenly divided by 1 and itself.  Sooooo
            //  we are gonna try to divide it by every other number
            //  If we are successful (even divisible) It is NOT PRIME
            //  If after this, it is still marked prime,
            //  Add it to the list
            foreach (var x in nums)
            {
                bool IsPrime = true;
                for (int i = 2; i < x; i++)
                {
                    if (x % i == 0)
                    {
                        IsPrime = false;
                        break;
                    }

                }
                if (IsPrime)
                {
                    primes.Add(x);
                }
            }
            sw.Stop();
            // Print how long it took
            Console.WriteLine("Normal {0}", sw.Elapsed);

            // Reset the stopwatch to zero
            sw.Reset();
            sw.Start();
            primes = new List<int>();

            // EXACTLY the same except we tell .NET
            //  Use ALL resources available.  THROTTLE the CPU
            //  and get this done ASAP.
            Parallel.ForEach(nums, (x) =>
                {
                    bool IsPrime = true;
                    for (int i = 2; i < x; i++)
                    {
                        if (x % i == 0)
                        {
                            IsPrime = false;
                            break;
                        }
                    }
                    if (IsPrime)
                    {
                        primes.Add(x);
                    }
                });
            sw.Stop();
            Console.WriteLine("PLINQ {0}", sw.Elapsed);
        }
    }
}

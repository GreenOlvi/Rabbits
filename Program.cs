using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Rabbits
{
    internal static class Program
    {
        internal static void Main()
        {
            const int gens = 99;

            var config = RabbitConfig.Create()
                .WithDieAfter(16)
                .WithMatureAfter(1)
                .WithChildrenCount(1);

            var popCount = new PopulationCount(config);

            var rabbits = new[] { new Rabbit(config) };
            PrintStats(1, rabbits, popCount);

            for (var i = 2; i <= gens; i++)
            {
                //rabbits = UpdateRabbits(rabbits);
                PrintStats(i, rabbits, popCount);
            }

            Console.ReadLine();
        }

        private static Rabbit[] UpdateRabbits(IEnumerable<Rabbit> rabbits) =>
            rabbits.SelectMany(r => r.Update()).ToArray();

        private static void PrintStats(int gen, Rabbit[] rabbits, PopulationCount popCount)
        {
            var alive = rabbits.Count(r => r.State != RabbitState.Dead);
            var died = rabbits.Count(r => r.State == RabbitState.Dead);
            var born = rabbits.Count(r => r.Age == 0);

            var sb = new StringBuilder();
            sb.AppendFormat("Gen={0}, ", gen);
            //sb.AppendFormat("Alive={0}, ", alive);
            //sb.AppendFormat("Born={0}, ", born);
            //sb.AppendFormat("Died={0}, ", died);
            //sb.AppendFormat("Fib({0})={1}, ", gen, Fib(gen));

            sb.AppendFormat("P({0})={1}", gen, popCount.AliveAt(gen));

            Console.WriteLine(sb.ToString());
            //PrintRabbits(rabbits);
        }

        private static void PrintRabbits(IEnumerable<Rabbit> rabbits)
        {
            Console.WriteLine(string.Join(", ", rabbits.Select(r => r.ToString())));
        }

        private static readonly Func<int, BigInteger> Fib = Memoizer.Memoize<int, BigInteger>(Fibonacci);
        private static BigInteger Fibonacci(int n)
        {
            if (n < 0) return 0;
            if (n <= 2) return 1;
            return Fib(n - 1) + Fib(n - 2);
        }
    }
}

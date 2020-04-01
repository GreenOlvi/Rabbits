using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    internal static class Program
    {
        internal static void Main()
        {
            const int gens = 20;

            var config = RabbitConfig.Create()
                .WithDieAfter(4)
                .WithMatureAfter(1)
                .WithChildrenCount(1);

            var rabbits = new[] { new Rabbit(config) };
            PrintStats(1, rabbits);

            for (var i = 2; i <= gens; i++)
            {
                rabbits = UpdateRabbits(rabbits);
                PrintStats(i, rabbits);
            }
        }

        private static Rabbit[] UpdateRabbits(IEnumerable<Rabbit> rabbits) =>
            rabbits.SelectMany(r => r.Update()).ToArray();

        private static void PrintStats(int gen, Rabbit[] rabbits)
        {
            var alive = rabbits.Count(r => r.State != RabbitState.Dead);
            var died = rabbits.Count(r => r.State == RabbitState.Dead);
            var born = rabbits.Count(r => r.Age == 0);

            var sb = new StringBuilder();
            sb.AppendFormat("Gen={0}, ", gen);
            sb.AppendFormat("Alive={0}, ", alive);
            sb.AppendFormat("Born={0}, ", born);
            sb.AppendFormat("Died={0}, ", died);
            sb.AppendFormat("Fib({0})={1}, ", gen, Fib(gen));
            Console.WriteLine(sb.ToString());
            //PrintRabbits(rabbits);
        }

        private static void PrintRabbits(IEnumerable<Rabbit> rabbits)
        {
            Console.WriteLine(string.Join(", ", rabbits.Select(r => r.ToString())));
        }

        private static readonly IDictionary<long, long> FibCache = new Dictionary<long, long>();
        private static long Fib(long n)
        {
            if (FibCache.ContainsKey(n))
            {
                return FibCache[n];
            }

            if (n <= 2)
            {
                return 1;
            }

            var result = Fib(n - 1) + Fib(n - 2);
            FibCache.Add(n, result);
            return result;
        }
    }
}

using System;
using System.Numerics;

namespace Rabbits
{
    public class PopulationCount
    {
        public PopulationCount(RabbitConfig config)
        {
            _config = config;
            AliveAt = Memoizer.Memoize<int, BigInteger>(Alive);
            DiedAt = Memoizer.Memoize<int, BigInteger>(Died);
            BornAt = Memoizer.Memoize<int, BigInteger>(Born);
        }

        private readonly RabbitConfig _config;

        public Func<int, BigInteger> AliveAt;
        public Func<int, BigInteger> DiedAt;
        public Func<int, BigInteger> BornAt;

        private BigInteger Alive(int generation)
        {
            if (generation < 1) return 0;
            if (generation == 1) return 1;

            return AliveAt(generation - 1) + BornAt(generation) - DiedAt(generation);
        }

        private BigInteger Died(int generation)
        {
            if (generation < 1) return 0;

            return BornAt(generation - _config.DieAfter);
        }

        private BigInteger Born(int generation)
        {
            if (generation < 1) return 0;
            if (generation == 1) return 1;

            var alive = AliveAt(generation - (_config.MatureAfter + 1));
            for (var i = 1; i < _config.MatureAfter + 1; i++)
            {
                alive -= DiedAt(generation - i);
            }

            return alive * _config.ChildrenCount;
        }
    }
}

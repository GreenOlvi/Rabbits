using System.Collections.Generic;

namespace Rabbits
{
    public class Rabbit
    {
        public Rabbit(RabbitConfig config)
        {
            _config = config;
            State = RabbitState.Child;
        }

        private readonly RabbitConfig _config;

        public int Age { get; private set; }
        public RabbitState State { get; private set; }

        public override string ToString() => $"[{Age}, {State}]";

        public IEnumerable<Rabbit> Update()
        {
            if (State == RabbitState.Dead)
            {
                yield break;
            }

            Age++;
            yield return this;

            if (State == RabbitState.Mature)
            {
                for (var i = 0; i < _config.ChildrenCount; i++)
                {
                    yield return new Rabbit(_config);
                }

                if (Age >= _config.DieAfter)
                {
                    State = RabbitState.Dead;
                }
            }

            if (State == RabbitState.Child && Age >= _config.MatureAfter)
            {
                State = RabbitState.Mature;
            }
        }
    }
}
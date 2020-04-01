namespace Rabbits
{
    public readonly struct RabbitConfig
    {
        public static RabbitConfig Create() => new RabbitConfig(0, 0, 0);

        private RabbitConfig(int dieAfter, int matureAfter, int childrenCount)
        {
            DieAfter = dieAfter;
            MatureAfter = matureAfter;
            ChildrenCount = childrenCount;
        }

        public RabbitConfig WithDieAfter(int dieAfter) => new RabbitConfig(dieAfter, MatureAfter, ChildrenCount);
        public RabbitConfig WithMatureAfter(int matureAfter) => new RabbitConfig(DieAfter, matureAfter, ChildrenCount);
        public RabbitConfig WithChildrenCount(int childrenCount) => new RabbitConfig(DieAfter, MatureAfter, childrenCount);

        public readonly int DieAfter;
        public readonly int MatureAfter;
        public readonly int ChildrenCount;
    }
}
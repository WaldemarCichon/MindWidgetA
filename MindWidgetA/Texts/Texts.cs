using System;
namespace MindWidgetA.Texts
{
    public abstract class Texts
    {
        protected abstract String[] Entries { get; }
        private Random randomizer;
        public String Last { get; private set; }

        public Texts()
        {
            randomizer = new Random();
        }

        public String Random
        {
            get
            {
                var index = randomizer.Next(Entries.Length);
                Last = Entries[index];
                return Last;
            }
        }

    }
}

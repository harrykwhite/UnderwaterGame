using System;

namespace UnderwaterGame.Options
{
    public abstract partial class Option
    {
        public enum Format
        {
            Percent,
            Toggle
        }

        public byte id;
        public float value;

        public string Name { get; protected set; }

        public float ValueMin { get; protected set; }
        public float ValueMax { get; protected set; }
        public bool ValueRounded { get; protected set; }
        public Format ValueFormat { get; protected set; }

        public bool Toggle => value == 1f;

        public Option()
        {
            Options.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Option
        {
            T option = Activator.CreateInstance<T>();
            option.id = id;

            return option;
        }

        public static Option GetOptionByID(byte id)
        {
            return Options.Find((Option option) => option.id == id);
        }

        protected abstract void Init();
    }
}
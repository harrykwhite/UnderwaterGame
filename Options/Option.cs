namespace UnderwaterGame.Options
{
    using System;

    public abstract partial class Option
    {
        public enum Format
        {
            Percent,

            Toggle
        }

        public byte id;

        public float value;

        public string name;

        public float valueMin;

        public float valueMax;

        public bool valueRounded;

        public Format valueFormat;

        public Option()
        {
            options.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Option
        {
            T option = Activator.CreateInstance<T>();
            option.id = id;
            return option;
        }

        public static Option GetOptionById(byte id)
        {
            return options.Find((Option option) => option.id == id);
        }

        protected abstract void Init();

        public bool GetToggle()
        {
            return value == 1f;
        }
    }
}
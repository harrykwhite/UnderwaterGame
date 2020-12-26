namespace UnderwaterGame.Environmentals
{
    using System;
    using UnderwaterGame.Sprites;

    public abstract partial class Environmental
    {
        public byte id;

        public Sprite sprite;

        public Environmental()
        {
            environmentals.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Environmental
        {
            T environmental = Activator.CreateInstance<T>();
            environmental.id = id;
            return environmental;
        }

        public static Environmental GetEnvironmentalById(byte id)
        {
            return environmentals.Find((Environmental environmental) => environmental.id == id);
        }

        public abstract void Init();
    }
}
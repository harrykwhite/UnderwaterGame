using System;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Environmentals
{
    public abstract partial class Environmental
    {
        public byte id;

        public Sprite Sprite { get; protected set; }

        public static float Speed => 0.025f;
        public static float Depth => 0.35f;

        public Environmental()
        {
            Environmentals.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Environmental
        {
            T environmental = Activator.CreateInstance<T>();
            environmental.id = id;

            return environmental;
        }

        public static Environmental GetEnvironmentalByID(byte id)
        {
            return Environmentals.Find((Environmental environmental) => environmental.id == id);
        }

        public abstract void Init();
    }
}
using System;
using UnderwaterGame.Options;

namespace UnderwaterGame
{
    [Serializable]
    public class Config
    {
        public float[] values;

        public Config()
        {
            values = new float[Option.Options.Count];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Option.GetOptionByID((byte)(i + 1)).value;
            }
        }
    }
}
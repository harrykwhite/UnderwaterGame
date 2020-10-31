namespace UnderwaterGame
{
    using System;
    using UnderwaterGame.Options;

    [Serializable]
    public class Config
    {
        public float[] values;

        public Config()
        {
            values = new float[Option.options.Count];
            for(int i = 0; i < values.Length; i++)
            {
                values[i] = Option.GetOptionById((byte)(i + 1)).value;
            }
        }
    }
}
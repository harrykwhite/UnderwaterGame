using System;
using UnderwaterGame.Environmentals;

namespace UnderwaterGame.Worlds
{
    [Serializable]
    public class WorldEnvironmental
    {
        public byte id;

        public int x;
        public int y;

        public Environmental EnvironmentalType => Environmental.GetEnvironmentalByID(id);
    }
}
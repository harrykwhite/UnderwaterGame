namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities.Characters;

    public class HitData
    {
        public float damage;

        public float strength;

        public Vector2 at;

        public float? direction;

        public bool hitEnemy;

        public bool hitPlayer;

        public Action<CharacterEntity> hitAction;
    }
}
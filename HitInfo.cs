namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities.Characters;

    public class HitInfo
    {
        public float damage;

        public Vector2 at;

        public float direction;

        public bool hitEnemy;

        public bool hitPlayer;

        public Action<CharacterEntity> hitAction;
    }
}
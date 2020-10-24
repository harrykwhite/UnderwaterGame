using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities.Characters;

namespace UnderwaterGame
{
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
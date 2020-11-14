﻿using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities.Characters.Enemies
{
    public abstract class EnemyCharacter : CharacterEntity, IHitCharacter
    {
        public WorldHotspot hotspot;

        public float touchDamage;

        public bool touchDamageEnemy;

        public bool touchDamagePlayer;

        public HitData HitCharacter(Entity target)
        {
            return new HitData { damage = touchDamage, at = target.position, direction = null, hitPlayer = touchDamagePlayer, hitEnemy = touchDamageEnemy };
        }

        public override void Kill()
        {
            base.Kill();
            if(hotspot.count > 0)
            {
                hotspot.count--;
            }
        }
    }
}
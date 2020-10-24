using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities.Characters;
using UnderwaterGame.Utilities;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities.Projectiles
{
    public abstract class ProjectileEntity : Entity, IHitCharacter
    {
        public float damage;

        public bool hitEnemy;
        public bool hitPlayer;

        public float speed;
        public float speedChange;
        public float speedMult = 1f;

        public float direction;
        public float directionChange;
        public float? directionInit;

        public float angleChange;
        public bool angleChangeRelative;
        public bool angleRelative = true;

        public bool pierce;
        public bool left;

        public bool hasGravity = true;

        protected void UpdateProjectile()
        {
            if (directionInit == null)
            {
                directionInit = direction;
            }

            speed += speedChange;
            direction += directionChange;
            angle += angleChange * (angleChangeRelative ? (float)Math.Cos(direction) : 1f);

            speed *= speedMult;

            if (angleRelative)
            {
                angle = direction;
            }

            if (TileCollision(position, World.TilemapType.Solids))
            {
                HitTile();
                Destroy();
            }

            if (!InWorld(position))
            {
                Destroy();
            }

            velocity = MathUtilities.LengthDirection(speed, directionInit.Value);

            if (hasGravity)
            {
                UpdateGravity(true);
                velocity.Y += gravity;
            }

            direction = MathUtilities.PointDirection(Vector2.Zero, velocity);
        }

        public HitInfo HitCharacter(Entity target)
        {
            HitInfo hitInfo = new HitInfo();

            hitInfo.damage = damage;
            hitInfo.at = target.position;
            hitInfo.direction = direction;

            hitInfo.hitEnemy = hitEnemy;
            hitInfo.hitPlayer = hitPlayer;
            hitInfo.hitAction = delegate (CharacterEntity character)
            {
                HitCharacter(character);

                if (!pierce)
                {
                    Destroy();
                }
            };

            return hitInfo;
        }

        protected virtual void Hit()
        {

        }

        protected virtual void HitCharacter(CharacterEntity character)
        {
            Hit();
        }

        protected virtual void HitTile()
        {
            Hit();
        }
    }
}
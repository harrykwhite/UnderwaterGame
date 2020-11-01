namespace UnderwaterGame.Entities.Projectiles
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class ProjectileEntity : Entity, IHitCharacter
    {
        public float damage;

        public bool hitEnemy;

        public bool hitPlayer;

        public float speed;

        public float direction;

        public float directionAcc;

        public float? directionInit;

        public float angleAcc;

        public bool angleRelative = true;

        public bool pierce;

        public bool left;

        public bool hasGravity = true;

        protected void UpdateProjectile()
        {
            if(directionInit == null)
            {
                directionInit = direction;
            }
            direction += directionAcc;
            angle += angleAcc * (float)Math.Cos(direction);
            if(angleRelative)
            {
                angle = direction;
            }
            if(TileCollision(position, World.Tilemap.Solids))
            {
                HitTile();
                Destroy();
            }
            if(!InWorld(position))
            {
                Destroy();
            }
            velocity = MathUtilities.LengthDirection(speed, directionInit.Value);
            if(hasGravity)
            {
                UpdateGravity(true);
                velocity.Y += gravity;
            }
            direction = MathUtilities.PointDirection(Vector2.Zero, velocity);
        }

        public HitInfo HitCharacter(Entity target)
        {
            HitInfo hitInfo = new HitInfo { damage = damage, at = target.position, direction = direction, hitEnemy = hitEnemy, hitPlayer = hitPlayer, hitAction = delegate (CharacterEntity character) { HitCharacter(character); if(!pierce) { Destroy(); } } };
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
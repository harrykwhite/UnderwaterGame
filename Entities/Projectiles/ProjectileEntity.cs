namespace UnderwaterGame.Entities.Projectiles
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class ProjectileEntity : Entity
    {
        public int damage;

        public float strength;

        public bool hitEnemy;

        public bool hitPlayer;

        public float speed;

        public float direction;

        public float directionAcc;

        public float? directionInit;

        public float angleAcc;

        public bool angleRelative = true;

        public bool left;

        protected int bloodParticleCount = 3;

        protected Color bloodParticleColor = Color.White;
        
        protected bool pierce;
        
        protected void UpdateProjectile()
        {
            if(directionInit == null)
            {
                directionInit = direction;
            }
            direction += directionAcc;
            angle += angleAcc * ((float)Math.Cos(direction) >= 0f ? 1f : -1f);
            if(angleRelative)
            {
                angle = direction;
            }
            velocity = MathUtilities.LengthDirection(speed, directionInit.Value);
            UpdateGravity(true);
            velocity.Y += gravity;
            direction = MathUtilities.PointDirection(Vector2.Zero, velocity);
            List<Entity> characterEntities = EntityManager.entities.FindAll((Entity entity) => entity is CharacterEntity);
            for(float i = 0f; i < velocity.Length(); i += Math.Min(1f, velocity.Length() - i))
            {
                bool hitCharacter = false;
                foreach(Entity characterEntity in characterEntities)
                {
                    CharacterEntity character = (CharacterEntity)characterEntity;
                    if((hitEnemy && character is EnemyCharacter) || (hitPlayer && character is PlayerCharacter))
                    {
                        if(collider.IsTouching(position, character.collider))
                        {
                            if(character.Hurt(new Hit(damage, strength, position, direction)))
                            {
                                if(character.health <= 0)
                                {
                                    character.Kill();
                                }
                                Hit();
                                if(!pierce)
                                {
                                    Destroy();
                                }
                                hitCharacter = true;
                                break;
                            }
                        }
                    }
                }
                if(hitCharacter)
                {
                    break;
                }
                if(TileCollision(position, World.Tilemap.Solids))
                {
                    Hit();
                    Destroy();
                    break;
                }
                position += Vector2.Normalize(velocity) * Math.Min(1f, velocity.Length() - i);
            }
        }

        protected virtual void Hit()
        {
            for(int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.speed = speed / 2f;
                blood.direction = direction - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((bloodParticleCount - 1f) / 2f)));
                blood.blend = bloodParticleColor;
            }
        }
    }
}
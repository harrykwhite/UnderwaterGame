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
        public int lifeMax = 300;

        public int damage;

        public float strength;

        public bool hitEnemy;

        public bool hitPlayer;

        public float speed;

        public float speedAcc;

        public float direction;

        public float directionAcc;

        public float angleAcc;

        public bool angleRelative = true;

        public bool left;

        protected int bloodParticleCount = 3;

        protected Color bloodParticleColor = Color.White;
        
        protected bool pierce;

        public float alphaAcc = 0.1f;
        
        protected void UpdateProjectile()
        {
            direction += directionAcc;
            angle = angleRelative ? direction : angle + (angleAcc * ((float)Math.Cos(direction) >= 0f ? 1f : -1f));
            if(life > lifeMax)
            {
                if(speed > 0f)
                {
                    speed -= Math.Min(speedAcc, speed);
                }
                else
                {
                    if(alpha > 0f)
                    {
                        alpha -= Math.Min(alphaAcc, alpha);
                    }
                    else
                    {
                        Destroy();
                    }
                }
            }
            else
            {
                if(alpha < 1f)
                {
                    alpha += Math.Min(alphaAcc, 1f - alpha);
                }
            }
            velocity = MathUtilities.LengthDirection(speed, direction);
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
namespace UnderwaterGame.Entities.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Utilities;

    public abstract class CharacterEntity : Entity
    {
        public int flashTime;

        public int flashTimeMax = 20;

        protected SoundEffect hurtSound;

        protected SoundEffect deathSound;

        protected int bloodParticleCount = 3;

        protected Color bloodParticleColor = new Color(204, 81, 81);

        public int health;

        public int healthMax;

        public float healthOffset = 32f;

        public int defense;

        public float knockbackSpeed;

        public float knockbackSpeedAcc = 0.1f;

        public float knockbackDirection;

        protected void UpdateStatus()
        {
            if(flashTime > 0)
            {
                flashTime--;
            }
            if(knockbackSpeed > 0f)
            {
                knockbackSpeed -= Math.Min(knockbackSpeedAcc, knockbackSpeed);
            }
            velocity += MathUtilities.LengthDirection(knockbackSpeed, knockbackDirection);
        }

        protected void DrawFlash()
        {
            DrawSelf(sprite.texturesFilled[(int)animator.index], color: Color.White * ((float)(flashTime * 2f) / (float)flashTimeMax), depth: depth + 0.001f);
        }

        protected void CheckForDamage()
        {
            List<Entity> hitCharacterEntities = EntityManager.entities.FindAll((Entity entity) => entity is IHitCharacter);
            foreach(Entity hitCharacterEntity in hitCharacterEntities)
            {
                IHitCharacter hitCharacter = (IHitCharacter)hitCharacterEntity;
                HitData hitData = hitCharacter.HitCharacter(this);
                if(this is PlayerCharacter && !hitData.hitPlayer)
                {
                    continue;
                }
                if(this is EnemyCharacter && !hitData.hitEnemy)
                {
                    continue;
                }
                if(hitCharacterEntity.collider.IsTouching(hitCharacterEntity.position, collider))
                {
                    Hurt(hitData);
                    if(health <= 0)
                    {
                        Kill();
                        break;
                    }
                }
            }
        }

        public virtual bool Hurt(HitData hitData)
        {
            if(flashTime > 0 || health <= 0)
            {
                return false;
            }
            int amount = Math.Max(hitData.damage - defense, 1);
            health -= amount;
            health = MathUtilities.Clamp(health, 0, healthMax);
            flashTime = flashTimeMax;
            knockbackSpeedAcc = 0.1f;
            if(hitData.direction != null)
            {
                knockbackSpeed += hitData.strength;
                knockbackDirection = hitData.direction.Value;
            }
            hitData.hitAction?.Invoke(this);
            Camera.Shake(2f, position);
            if(hurtSound != null)
            {
                SoundUtilities.PlaySound(hurtSound);
            }
            if(health > 0)
            {
                float bloodParticleDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
                for(int i = 0; i < bloodParticleCount; i++)
                {
                    Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                    blood.direction = hitData.direction == null ? (((MathHelper.Pi * 2f) / bloodParticleCount) * i) + bloodParticleDirectionOffset : hitData.direction.Value - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((bloodParticleCount - 1f) / 2f)));
                    blood.blend = bloodParticleColor;
                }
            }
            return true;
        }

        public virtual bool Heal(int amount)
        {
            if(flashTime > 0 || health >= healthMax)
            {
                return false;
            }
            health += amount;
            health = MathUtilities.Clamp(health, 0, healthMax);
            flashTime = flashTimeMax;
            float bloodParticleDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
            for(int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = (((MathHelper.Pi * 2f) / bloodParticleCount) * i) + bloodParticleDirectionOffset;
            }
            return true;
        }

        public virtual void Kill()
        {
            Destroy();
            Camera.Shake(4f, position);
            int particleCount = bloodParticleCount * 2;
            float particleDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
            for(int i = 0; i < particleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = (((MathHelper.Pi * 2f) / particleCount) * i) + particleDirectionOffset;
                blood.blend = bloodParticleColor;
            }
            if(deathSound != null)
            {
                SoundUtilities.PlaySound(deathSound);
            }
        }
    }
}
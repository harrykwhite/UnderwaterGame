namespace UnderwaterGame.Entities.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using System;
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

        public virtual bool Hurt(Hit hit)
        {
            if(flashTime > 0 || health <= 0)
            {
                return false;
            }
            int amount = Math.Max(hit.damage - defense, 1);
            health -= amount;
            health = MathUtilities.Clamp(health, 0, healthMax);
            flashTime = flashTimeMax;
            knockbackSpeedAcc = 0.1f;
            if(hit.strength > 0f && hit.direction != null)
            {
                knockbackSpeed += hit.strength;
                knockbackDirection = hit.direction.Value;
            }
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
                    blood.direction = hit.direction == null ? (((MathHelper.Pi * 2f) / bloodParticleCount) * i) + bloodParticleDirectionOffset : hit.direction.Value - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((bloodParticleCount - 1f) / 2f)));
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
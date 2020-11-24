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
        protected int flashTime;

        protected int flashTimeMax = 16;

        protected SoundEffect hurtSound;

        protected SoundEffect deathSound;

        protected int bloodParticleCount = 3;

        protected Color bloodParticleColor = new Color(204, 81, 81);

        public float health;

        public float healthMax;

        public float defense;

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
            if(health <= 0f)
            {
                Kill();
            }
        }

        protected void DrawFlash()
        {
            DrawSelf(sprite.texturesFilled[(int)animator.index], color: Color.White * ((float)(flashTime * 2f) / (float)flashTimeMax), depth: depth + 0.001f);
        }

        protected void CheckForDamage(Collider hit)
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
                if(hitCharacterEntity.collider.IsTouching(hitCharacterEntity.position, hit))
                {
                    Hurt(hitData);
                }
            }
        }

        public virtual bool Hurt(HitData hitData)
        {
            if(flashTime > 0 || health <= 0f)
            {
                return false;
            }
            float amount = Math.Max(hitData.damage - defense, 1f);
            health -= amount;
            health = MathUtilities.Clamp(health, 0f, healthMax);
            flashTime = flashTimeMax;
            knockbackSpeedAcc = 0.1f;
            if(hitData.direction != null)
            {
                knockbackSpeed += amount / 2f;
                knockbackDirection = hitData.direction.Value;
            }
            hitData.hitAction?.Invoke(this);
            Camera.Shake(2f, position);
            if(hurtSound != null)
            {
                SoundUtilities.PlaySound(hurtSound);
            }
            FloatingTextEntity floatingTextEntity = (FloatingTextEntity)EntityManager.AddEntity<FloatingTextEntity>(position);
            floatingTextEntity.text = "-" + amount.ToString();
            floatingTextEntity.speed = 4f;
            floatingTextEntity.direction = -MathHelper.Pi / 2f;
            if(health > 0f)
            {
                for(int i = 0; i < bloodParticleCount; i++)
                {
                    Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                    blood.direction = hitData.direction == null ? ((MathHelper.Pi * 2f) / bloodParticleCount) * i : hitData.direction.Value - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((bloodParticleCount - 1f) / 2f)));
                    blood.blend = bloodParticleColor;
                }
            }
            return true;
        }

        public virtual bool Heal(float amount)
        {
            if(flashTime > 0 || health >= healthMax)
            {
                return false;
            }
            amount = Math.Max(amount, 0f);
            health += amount;
            health = MathUtilities.Clamp(health, 0f, healthMax);
            flashTime = flashTimeMax;
            FloatingTextEntity floatingTextEntity = (FloatingTextEntity)EntityManager.AddEntity<FloatingTextEntity>(position);
            floatingTextEntity.text = "+" + amount.ToString();
            floatingTextEntity.speed = 4f;
            floatingTextEntity.direction = -MathHelper.Pi / 2f;
            for(int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = ((MathHelper.Pi * 2f) / bloodParticleCount) * i;
            }
            return true;
        }

        public virtual void Kill()
        {
            Destroy();
            Camera.Shake(4f, position);
            int particleCount = bloodParticleCount * 2;
            for(int i = 0; i < particleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = ((MathHelper.Pi * 2f) / particleCount) * i;
                blood.blend = bloodParticleColor;
            }
            if(deathSound != null)
            {
                SoundUtilities.PlaySound(deathSound);
            }
        }
    }
}
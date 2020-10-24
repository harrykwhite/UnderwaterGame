using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UnderwaterGame.Entities.Characters.Enemies;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Sound;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Entities.Characters
{
    public abstract class CharacterEntity : Entity
    {
        protected bool flicker;
        protected int flickerTime;
        protected int flickerTimeMax = 8;

        protected int invincibleTime;
        protected int invincibleTimeMax = 24;

        protected int flashTime;
        protected int flashTimeMax = 12;

        protected SoundEffect hurtSound;
        protected SoundEffect deathSound;

        protected int bloodParticleCount = 3;
        protected Color bloodParticleColor = new Color(204, 81, 81);

        public float Health { get; protected set; }
        public float HealthMax { get; protected set; }

        public float Defense { get; protected set; }

        public bool Invincible => invincibleTime > 0;
        public bool Flash => flashTime > 0;

        public new void DrawSelf(Texture2D texture = null, Vector2? position = null, Rectangle? sourceRectangle = null, Color? color = null, float? rotation = null, Vector2? origin = null, Vector2? scale = null, SpriteEffects? flip = null, float? depth = null)
        {
            Texture2D drawTexture = texture;
            int drawTextureIndex = (int)(Animator?.index ?? 0f);

            Color drawColor = color ?? blend;
            float drawAlpha = 1f;

            if (drawTexture == null)
            {
                if (Flash)
                {
                    drawTexture = Animator.sprite.TexturesFilled[drawTextureIndex];
                    drawColor = Color.White;
                }
                else
                {
                    drawTexture = Animator.sprite.Textures[drawTextureIndex];

                    if (Invincible)
                    {
                        drawAlpha = (flicker ? 1f : 2f) / 3f;
                    }
                }
            }

            base.DrawSelf(drawTexture, position, sourceRectangle, drawColor * drawAlpha * alpha, rotation, origin, scale, flip, depth);
        }

        protected void UpdateStatus()
        {
            if (Invincible)
            {
                if (flickerTime > 0)
                {
                    flickerTime--;
                }
                else
                {
                    flicker = !flicker;
                    flickerTime = flickerTimeMax;
                }

                invincibleTime--;
            }
            else
            {
                flicker = false;
                flickerTime = 0;
            }

            if (flashTime > 0)
            {
                flashTime--;
            }

            if (Health <= 0f)
            {
                Kill();
            }
        }



        protected void CheckForDamage(Collider hit)
        {
            List<Entity> hitCharacterEntities = EntityManager.Entities.FindAll((Entity entity) => entity is IHitCharacter);

            foreach (Entity hitCharacterEntity in hitCharacterEntities)
            {
                IHitCharacter hitCharacter = (IHitCharacter)hitCharacterEntity;
                HitInfo hitInfo = hitCharacter.HitCharacter(this);

                if (this is PlayerCharacter && !hitInfo.hitPlayer)
                {
                    continue;
                }

                if (this is EnemyCharacter && !hitInfo.hitEnemy)
                {
                    continue;
                }

                if (hitCharacterEntity.Collider.IsTouching(hitCharacterEntity.position, hit))
                {
                    Hurt(hitInfo);
                }
            }
        }

        public virtual bool Hurt(HitInfo hitInfo)
        {
            if (Invincible || Health <= 0f)
            {
                return false;
            }

            float damage = Math.Max(hitInfo.damage - Defense, 0f);

            Health -= damage;
            Health = MathUtilities.Clamp(Health, 0f, HealthMax);

            invincibleTime = invincibleTimeMax;
            flickerTime = flickerTimeMax;
            flashTime = flashTimeMax;

            hitInfo.hitAction?.Invoke(this);

            Camera.Shake(2f, position);

            if (hurtSound != null)
            {
                SoundManager.PlaySound(hurtSound, SoundManager.Category.Sound);
            }

            FloatingTextEntity floatingTextEntity = (FloatingTextEntity)EntityManager.AddEntity<FloatingTextEntity>(position);
            floatingTextEntity.text = Math.Floor(-damage).ToString();
            floatingTextEntity.speed = RandomUtilities.Range(3.5f, 4f);
            floatingTextEntity.direction = -MathHelper.Pi / 2f;

            for (int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = hitInfo.direction - ((MathHelper.Pi / 12f) * 13f) + (((float)i / (float)bloodParticleCount) * (MathHelper.Pi / 6f));
                blood.blend = bloodParticleColor;
            }

            return true;
        }

        public virtual bool Heal(float amount)
        {
            if (Invincible || Health >= HealthMax)
            {
                return false;
            }

            amount = Math.Max(amount, 0f);

            Health += amount;
            Health = MathUtilities.Clamp(Health, 0f, HealthMax);

            invincibleTime = invincibleTimeMax;
            flickerTime = flickerTimeMax;
            flashTime = flashTimeMax;

            FloatingTextEntity floatingTextEntity = (FloatingTextEntity)EntityManager.AddEntity<FloatingTextEntity>(position);
            floatingTextEntity.text = "+" + Math.Floor(amount).ToString();
            floatingTextEntity.speed = RandomUtilities.Range(3.5f, 4f);
            floatingTextEntity.direction = -MathHelper.Pi / 2f;

            for (int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.speed /= 2f;
                blood.direction = ((MathHelper.Pi * 2f) / bloodParticleCount) * i;
                blood.blend = new Color(226, 226, 226);
            }

            return true;
        }

        public virtual void Kill()
        {
            Destroy();

            Camera.Shake(4f, position);

            for (int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.speed /= 2f;
                blood.direction = ((MathHelper.Pi * 2f) / bloodParticleCount) * i;
                blood.blend = bloodParticleColor;
            }

            if (deathSound != null)
            {
                SoundManager.PlaySound(deathSound, SoundManager.Category.Sound);
            }
        }
    }
}
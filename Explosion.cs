using Microsoft.Xna.Framework;
using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Utilities;

namespace UnderwaterGame
{
    public static class Explosion
    {
        public static void Spawn(float damage, Vector2 at, Vector2 range, bool hitPlayer, bool hitEnemy, int particleCount = 5)
        {
            for (int i = 0; i < particleCount; i++)
            {
                Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(at);
                smoke.direction = ((MathHelper.Pi * 2f) / particleCount) * i;
            }

            HitEntity hitEntity = (HitEntity)EntityManager.AddEntity<HitEntity>(at);
            hitEntity.SetHitInfo(damage, hitEntity.position, RandomUtilities.Range(0f, MathHelper.Pi), hitPlayer, hitEnemy);
            hitEntity.SetScaleInfo((int)range.X, (int)range.Y);

            Camera.Shake(range.Length() / 10f, at);
        }
    }
}
using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities.Characters;

namespace UnderwaterGame.Entities
{
    public class HitEntity : Entity, IHitCharacter
    {
        private HitInfo hitInfo = new HitInfo();

        public int Width => Collider.shape.width;
        public int Height => Collider.shape.height;

        public override void Draw()
        {

        }

        public override void Init()
        {

        }

        public override void Update()
        {
            if (life >= 2)
            {
                Destroy();
            }
        }

        public HitInfo HitCharacter(Entity target) => hitInfo;

        public void SetHitInfo(float damage, Vector2 at, float direction, bool hitPlayer, bool hitEnemy, Action<CharacterEntity> hitAction = null)
        {
            hitInfo.damage = damage;

            hitInfo.at = at;
            hitInfo.direction = direction;

            hitInfo.hitPlayer = hitPlayer;
            hitInfo.hitEnemy = hitEnemy;
            hitInfo.hitAction = hitAction;
        }

        public void SetScaleInfo(int width, int height)
        {
            Collider.shape.width = width;
            Collider.shape.height = height;

            Collider.shape.Clear();
        }
    }
}
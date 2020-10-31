namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities.Characters;

    public class HitEntity : Entity, IHitCharacter
    {
        private HitInfo hitInfo = new HitInfo();

        public override void Draw()
        {
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(life >= 2)
            {
                Destroy();
            }
        }

        public HitInfo HitCharacter(Entity target)
        {
            return hitInfo;
        }

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
            collider.shape.width = width;
            collider.shape.height = height;
            collider.shape.Clear();
        }
    }
}
namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities.Characters;

    public class HitEntity : Entity, IHitCharacter
    {
        public HitData hitData = new HitData();

        public override void Draw()
        {
            //Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, position, null, Color.Red, 0f, new Vector2(0.5f), new Vector2(collider.shape.width, collider.shape.height), SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            collider = new Collider(new Shape(Shape.Fill.Rectangle, 16, 16), this);
        }

        public override void Update()
        {
            if(life > 0)
            {
                Destroy();
            }
        }

        public HitData HitCharacter(Entity target)
        {
            return hitData;
        }

        public void SetHitData(int damage, float strength, Vector2 at, float direction, bool hitPlayer, bool hitEnemy, Action<CharacterEntity> hitAction = null)
        {
            hitData.damage = damage;
            hitData.strength = strength;
            hitData.at = at;
            hitData.direction = direction;
            hitData.hitPlayer = hitPlayer;
            hitData.hitEnemy = hitEnemy;
            hitData.hitAction = hitAction;
        }
    }
}
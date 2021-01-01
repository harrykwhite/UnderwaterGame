namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class PinkJellyShuriken : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.pinkJellyShuriken, true);
            depth = 0.7125f;
            angleAcc = MathHelper.Pi / 36f;
            angleRelative = false;
            speed = 8f;
            pierce = true;
            bloodParticleColor = new Color(216, 143, 172);
        }

        public override void Update()
        {
            UpdateProjectile();
            velocity = Vector2.Zero;
        }
    }
}
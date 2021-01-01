namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;

    public class PurpleJellyShuriken : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.purpleJellyShuriken, true);
            depth = 0.7125f;
            angleAcc = MathHelper.Pi / 36f;
            angleRelative = false;
            speed = 8f;
            pierce = true;
            bloodParticleColor = new Color(164, 132, 183);
        }

        public override void Update()
        {
            UpdateProjectile();
            velocity = Vector2.Zero;
        }
    }
}
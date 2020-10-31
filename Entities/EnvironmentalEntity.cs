namespace UnderwaterGame.Entities
{
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Worlds;

    public class EnvironmentalEntity : Entity
    {
        public Environmental environmentalType;

        public WorldEnvironmental worldEnvironmental;

        public void SetEnvironmental(Environmental environmentalType, WorldEnvironmental worldEnvironmental)
        {
            this.environmentalType = environmentalType;
            this.worldEnvironmental = worldEnvironmental;
            SetSprite(this.environmentalType.sprite);
            animator = new Animator(sprite) { speed = Environmental.speed };
        }

        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            depth = Environmental.depth;
        }

        public override void Update()
        {
            animator.Update();
        }
    }
}
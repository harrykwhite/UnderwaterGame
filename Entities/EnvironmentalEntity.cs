using UnderwaterGame.Environmentals;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities
{
    public class EnvironmentalEntity : Entity
    {
        public Environmental EnvironmentalType { get; private set; }
        public WorldEnvironmental WorldEnvironmental { get; private set; }

        public void SetEnvironmental(Environmental environmentalType, WorldEnvironmental worldEnvironmental)
        {
            EnvironmentalType = environmentalType;
            WorldEnvironmental = worldEnvironmental;

            SetSprite(EnvironmentalType.Sprite);

            Animator = new Animator(Sprite)
            {
                speed = Environmental.Speed
            };
        }

        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            depth = Environmental.Depth;
        }

        public override void Update()
        {
            Animator.Update();
        }
    }
}
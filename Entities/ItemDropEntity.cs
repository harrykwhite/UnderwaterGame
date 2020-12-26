using Microsoft.Xna.Framework;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Items;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities
{
    public class ItemDropEntity : Entity
    {
        public Item itemType;

        public int quantity;

        public void SetItem(Item itemType, int quantity)
        {
            this.itemType = itemType;
            this.quantity = quantity;
            if(this.itemType != null)
            {
                SetSprite(this.itemType.sprite, true);
                animator = new Animator(sprite);
            }
        }

        public override void Draw()
        {
            DrawSelf(texture: sprite.texturesOutlined[(int)animator.index], origin: new Vector2(sprite.texturesOutlined[(int)animator.index].Width, sprite.texturesOutlined[(int)animator.index].Height) / 2f);
        }

        public override void Init()
        {
            depth = 0.45f;
        }

        public override void Update()
        {
            if(World.player != null)
            {
                if(collider.IsTouching(position, World.player.collider))
                {
                    if(World.player.inventory.AddItem(itemType, quantity))
                    {
                        int particleCount = 3;
                        float particleDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
                        for(int i = 0; i < particleCount; i++)
                        {
                            Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                            blood.direction = (((MathHelper.Pi * 2f) / particleCount) * i) + particleDirectionOffset;
                        }
                        Destroy();
                    }
                }
            }
        }
    }
}
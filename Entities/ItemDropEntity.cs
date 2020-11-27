using Microsoft.Xna.Framework;
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
                SetSprite(this.itemType.sprite, false);
                animator = new Animator(sprite);
            }
        }

        public override void Draw()
        {
            DrawSelf(origin: new Vector2(sprite.textures[(int)animator.index].Width, sprite.textures[(int)animator.index].Height) / 2f);
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(collider.IsTouching(position, World.player.collider))
            {
                if(World.player.inventory.AddItem(itemType, quantity))
                {
                    Destroy();
                }
            }
        }
    }
}
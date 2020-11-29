namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Projectiles.Arrows;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class WoodenBow : BowRanged
    {
        protected override void Init()
        {
            name = "Wooden Bow";
            sprite = Sprite.woodenBow;
            useTime = 35;
            useAngleUpdate = true;
            damage = 4f;
        }

        public override void WhileUse()
        {
            if(World.player.heldItem.useState != 0 || (int)World.player.heldItem.animator.index != 3)
            {
                return;
            }
            Shoot<WoodenArrow>(World.player.heldItem.angleBase, 7f);
            World.player.heldItem.useState = 1;
        }
    }
}
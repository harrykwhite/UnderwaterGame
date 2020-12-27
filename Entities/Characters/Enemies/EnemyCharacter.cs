using UnderwaterGame.Items;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities.Characters.Enemies
{
    public abstract class EnemyCharacter : CharacterEntity
    {
        public Hotspot hotspot;

        public int touchDamage;

        protected Item[] itemDropType;

        protected int[] itemDropQuantity;

        protected float[] itemDropChance;

        public void UpdateTouchDamage()
        {
            if(collider.IsTouching(position, World.player.collider))
            {
                if(World.player.Hurt(new Hit(touchDamage, 0f, position, null)))
                {
                    if(World.player.health <= 0)
                    {
                        World.player.Kill();
                    }
                }
            }
        }
        
        public override void Kill()
        {
            base.Kill();
            if(itemDropType != null)
            {
                int? index = null;
                do
                {
                    for(int i = 0; i < itemDropType.Length; i++)
                    {
                        if(Main.random.Next(100) <= (itemDropChance[i] * 100f))
                        {
                            index = i;
                        }
                    }
                } while(index == null);
                ItemDropEntity itemDrop = (ItemDropEntity)EntityManager.AddEntity<ItemDropEntity>(position);
                itemDrop.SetItem(itemDropType[index.Value], itemDropQuantity[index.Value]);
            }
        }
    }
}
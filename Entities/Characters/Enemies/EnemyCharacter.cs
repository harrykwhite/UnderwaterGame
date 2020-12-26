using UnderwaterGame.Items;

namespace UnderwaterGame.Entities.Characters.Enemies
{
    public abstract class EnemyCharacter : CharacterEntity, IHitCharacter
    {
        public Hotspot hotspot;

        public int touchDamage;

        public bool touchDamageEnemy;

        public bool touchDamagePlayer;

        protected Item[] itemDropType;

        protected int[] itemDropQuantity;

        protected float[] itemDropChance;
        
        public HitData HitCharacter(Entity target)
        {
            return new HitData { damage = touchDamage, at = target.position, hitPlayer = touchDamagePlayer, hitEnemy = touchDamageEnemy };
        }

        public override void Kill()
        {
            base.Kill();
            if(hotspot != null)
            {
                if(hotspot.count > 0)
                {
                    hotspot.count--;
                    hotspot.countScale = hotspot.countScaleMax;
                }
            }
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
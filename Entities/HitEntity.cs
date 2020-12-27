namespace UnderwaterGame.Entities
{
    using System.Collections.Generic;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Entities.Characters.Enemies;

    public class HitEntity : Entity
    {
        public Hit hit;

        public bool hitEnemy;

        public bool hitPlayer;

        public override void Draw()
        {
        }

        public override void Init()
        {
            collider = new Collider(new Shape(Shape.Fill.Rectangle, 16, 16), this);
        }

        public override void Update()
        {
            List<Entity> characterEntities = EntityManager.entities.FindAll((Entity entity) => entity is CharacterEntity);
            foreach(Entity characterEntity in characterEntities)
            {
                CharacterEntity character = (CharacterEntity)characterEntity;
                if((hitEnemy && character is EnemyCharacter) || (hitPlayer && character is PlayerCharacter))
                {
                    if(collider.IsTouching(position, character.collider))
                    {
                        if(character.Hurt(hit))
                        {
                            if(character.health <= 0)
                            {
                                character.Kill();
                            }
                            break;
                        }
                    }
                }
            }
            Destroy();
        }
    }
}
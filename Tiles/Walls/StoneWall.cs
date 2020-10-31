namespace UnderwaterGame.Tiles.Walls
{
    public class StoneWall : WallTile
    {
        protected override void Init()
        {
            texture = stone.texture;
            textureBorder = stone.textureBorder;
        }

        public override void SetTextures()
        {
            textures = stone.textures;
        }
    }
}
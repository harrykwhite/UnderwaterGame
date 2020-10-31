namespace UnderwaterGame.Tiles.Walls
{
    public class SandWall : WallTile
    {
        protected override void Init()
        {
            texture = sand.texture;
            textureBorder = sand.textureBorder;
        }

        public override void SetTextures()
        {
            textures = sand.textures;
        }
    }
}
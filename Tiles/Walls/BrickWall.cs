namespace UnderwaterGame.Tiles.Walls
{
    public class BrickWall : WallTile
    {
        protected override void Init()
        {
            Texture = Brick.Texture;
            TextureBorder = Brick.TextureBorder;
        }

        public override void SetTextures()
        {
            Textures = Brick.Textures;
        }
    }
}
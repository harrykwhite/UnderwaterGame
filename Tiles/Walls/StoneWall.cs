namespace UnderwaterGame.Tiles.Walls
{
    public class StoneWall : WallTile
    {
        protected override void Init()
        {
            Texture = Stone.Texture;
            TextureBorder = Stone.TextureBorder;
        }

        public override void SetTextures()
        {
            Textures = Stone.Textures;
        }
    }
}
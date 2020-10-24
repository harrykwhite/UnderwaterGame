namespace UnderwaterGame.Tiles.Walls
{
    public class SandWall : WallTile
    {
        protected override void Init()
        {
            Texture = Sand.Texture;
            TextureBorder = Sand.TextureBorder;
        }

        public override void SetTextures()
        {
            Textures = Sand.Textures;
        }
    }
}
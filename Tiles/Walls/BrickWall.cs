namespace UnderwaterGame.Tiles.Walls
{
    public class BrickWall : WallTile
    {
        protected override void Init()
        {
            texture = brick.texture;
            textureBorder = brick.textureBorder;
        }

        public override void SetTextures()
        {
            textures = brick.textures;
        }
    }
}
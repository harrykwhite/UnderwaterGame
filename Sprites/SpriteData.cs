namespace UnderwaterGame.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using UnderwaterGame.Assets;
    using UnderwaterGame.Utilities;

    public partial class Sprite
    {
        private static Sprite Load(TextureLibrary.LibraryAsset libraryAsset)
        {
            Sprite sprite = new Sprite { libraryAsset = libraryAsset };
            if(!LoadTexture(sprite))
            {
                sprite.SetTexture(libraryAsset.asset);
            }
            if(!LoadBound(sprite))
            {
                LoadBoundDefault(sprite);
            }
            if(!LoadOrigin(sprite))
            {
                LoadOriginDefault(sprite);
            }
            if(!LoadShape(sprite))
            {
                LoadShapeDefault(sprite);
            }
            return sprite;
        }

        private static bool LoadTexture(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;
            if(libraryAsset == Main.textureLibrary.ENVIRONMENTALS_SEAWEED_SEAWEED || libraryAsset == Main.textureLibrary.CHARACTERS_PLAYER_PLAYERIDLE || libraryAsset == Main.textureLibrary.CHARACTERS_PLAYER_PLAYERSWIM || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_HEADS_STONEHELMETWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_CHESTS_STONECHESTPLATEWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_LEGS_STONELEGGINGSWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_FEET_STONEBOOTSWEAR || libraryAsset == Main.textureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.SetTextureSheet(libraryAsset.asset, 16);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.CHARACTERS_ENEMIES_JELLYFISH_JELLYFISH || libraryAsset == Main.textureLibrary.CHARACTERS_ENEMIES_JELLYFISH_TALLJELLYFISH)
            {
                sprite.SetTextureSheet(libraryAsset.asset, 15);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.EFFECTS_LONGSWING)
            {
                sprite.SetTextureSheet(libraryAsset.asset, 32);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.EFFECTS_WIDESWING)
            {
                sprite.SetTextureSheet(libraryAsset.asset, 32);
                return true;
            }
            return false;
        }

        private static bool LoadBound(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;
            if(libraryAsset == Main.textureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.bound = new Rectangle(6, 1, 3, 12);
                return true;
            }
            return false;
        }

        private static bool LoadOrigin(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;
            if(libraryAsset == Main.textureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.origin = new Vector2(6f, sprite.textures[0].Height / 2f);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT || libraryAsset == Main.textureLibrary.ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD)
            {
                sprite.origin = new Vector2(1f, sprite.textures[0].Height / 2f);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.EFFECTS_LONGSWING)
            {
                sprite.origin = new Vector2(26f, sprite.textures[0].Height / 2f);
                return true;
            }
            if(libraryAsset == Main.textureLibrary.EFFECTS_WIDESWING)
            {
                sprite.origin = new Vector2(18f, sprite.textures[0].Height / 2f);
                return true;
            }
            return false;
        }

        private static bool LoadShape(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;
            if(libraryAsset == Main.textureLibrary.CHARACTERS_PLAYER_PLAYERIDLE || libraryAsset == Main.textureLibrary.CHARACTERS_PLAYER_PLAYERSWIM || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_HEADS_STONEHELMETWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_CHESTS_STONECHESTPLATEWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_LEGS_STONELEGGINGSWEAR || libraryAsset == Main.textureLibrary.ITEMS_ARMOURS_FEET_STONEBOOTSWEAR)
            {
                sprite.shape = new Shape(Shape.Fill.Rectangle, 8, 8);
                return true;
            }
            return false;
        }

        private static void LoadBoundDefault(Sprite sprite)
        {
            int xMin = -1;
            int yMin = -1;
            int xMax = -1;
            int yMax = -1;
            for(int i = 0; i < sprite.textures.Length; i++)
            {
                Texture2D texture = sprite.textures[i];
                Color[] textureData = new Color[texture.Width * texture.Height];
                Color[,] textureData2D;
                texture.GetData(textureData);
                textureData2D = GeneralUtilities.AsTwoDimensional(textureData, texture.Width, texture.Height);
                for(int y = 0; y < texture.Height; y++)
                {
                    for(int x = 0; x < texture.Width; x++)
                    {
                        if(textureData2D[x, y].A > 0)
                        {
                            if(x < xMin || xMin == -1)
                            {
                                xMin = x;
                            }
                            if(y < yMin || yMin == -1)
                            {
                                yMin = y;
                            }
                            if(x > xMax || xMax == -1)
                            {
                                xMax = x;
                            }
                            if(y > yMax || yMax == -1)
                            {
                                yMax = y;
                            }
                        }
                    }
                }
            }
            sprite.bound = new Rectangle(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
        }

        private static void LoadOriginDefault(Sprite sprite)
        {
            sprite.origin = new Vector2(sprite.bound.X + (sprite.bound.Width / 2f), sprite.bound.Y + (sprite.bound.Height / 2f));
        }

        private static void LoadShapeDefault(Sprite sprite)
        {
            sprite.shape = new Shape(Shape.Fill.Rectangle, sprite.bound.Width, sprite.bound.Height);
        }
    }
}
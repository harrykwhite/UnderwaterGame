using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnderwaterGame.Assets;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Sprites
{
    public partial class Sprite
    {
        private static Sprite Load(TextureLibrary.LibraryAsset libraryAsset)
        {
            Sprite sprite = new Sprite
            {
                libraryAsset = libraryAsset
            };

            if (!LoadTexture(sprite))
            {
                sprite.SetTexture(libraryAsset.Asset);
            }

            if (!LoadBound(sprite))
            {
                LoadBoundDefault(sprite);
            }

            if (!LoadOrigin(sprite))
            {
                LoadOriginDefault(sprite);
            }

            if (!LoadShape(sprite))
            {
                LoadShapeDefault(sprite);
            }

            return sprite;
        }

        private static bool LoadTexture(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;

            if (libraryAsset == Main.TextureLibrary.ENVIRONMENTALS_BIGSEAWEED
                || libraryAsset == Main.TextureLibrary.ENVIRONMENTALS_SMALLSEAWEED
                || libraryAsset == Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERIDLE
                || libraryAsset == Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERSWIM
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMETWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATEWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGSWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTSWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.SetTextureSheet(libraryAsset.Asset, 16);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.CHARACTERS_ENEMIES_JELLYFISH_JELLYFISH
                || libraryAsset == Main.TextureLibrary.CHARACTERS_ENEMIES_JELLYFISH_TALLJELLYFISH)
            {
                sprite.SetTextureSheet(libraryAsset.Asset, 15);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.EFFECTS_LONGSWING)
            {
                sprite.SetTextureSheet(libraryAsset.Asset, 32);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.EFFECTS_WIDESWING)
            {
                sprite.SetTextureSheet(libraryAsset.Asset, 32);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.PARTICLES_BLOOD)
            {
                sprite.SetTextureSheet(libraryAsset.Asset, 3);
                return true;
            }

            return false;
        }

        private static bool LoadBound(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;

            if (libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.Bound = new Rectangle(6, 1, 3, 12);
                return true;
            }

            return false;
        }

        private static bool LoadOrigin(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;

            if (libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW)
            {
                sprite.Origin = new Vector2(6f, sprite.Height / 2f);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT
                || libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD
                || libraryAsset == Main.TextureLibrary.ITEMS_WEAPONS_MAGIC_WANDS_WOODENWAND)
            {
                sprite.Origin = new Vector2(1f, sprite.Height / 2f);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.EFFECTS_LONGSWING)
            {
                sprite.Origin = new Vector2(26f, sprite.Height / 2f);
                return true;
            }

            if (libraryAsset == Main.TextureLibrary.EFFECTS_WIDESWING)
            {
                sprite.Origin = new Vector2(18f, sprite.Height / 2f);
                return true;
            }

            return false;
        }

        private static bool LoadShape(Sprite sprite)
        {
            TextureLibrary.LibraryAsset libraryAsset = sprite.libraryAsset;

            if (libraryAsset == Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERIDLE
                || libraryAsset == Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERSWIM
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMETWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATEWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGSWEAR
                || libraryAsset == Main.TextureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTSWEAR)
            {
                sprite.Shape = new Shape(Shape.Fill.Rectangle, 8, 8);
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

            for (int i = 0; i < sprite.Textures.Length; i++)
            {
                Texture2D texture = sprite.Textures[i];

                Color[] textureData = new Color[texture.Width * texture.Height];
                Color[,] textureData2D;

                texture.GetData(textureData);
                textureData2D = GeneralUtilities.AsTwoDimensional(textureData, texture.Width, texture.Height);

                for (int y = 0; y < texture.Height; y++)
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        if (textureData2D[x, y].A > 0)
                        {
                            if (x < xMin || xMin == -1)
                            {
                                xMin = x;
                            }

                            if (y < yMin || yMin == -1)
                            {
                                yMin = y;
                            }

                            if (x > xMax || xMax == -1)
                            {
                                xMax = x;
                            }

                            if (y > yMax || yMax == -1)
                            {
                                yMax = y;
                            }
                        }
                    }
                }
            }

            sprite.Bound = new Rectangle(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
        }

        private static void LoadOriginDefault(Sprite sprite)
        {
            sprite.Origin = new Vector2(sprite.Bound.X + (sprite.Bound.Width / 2f), sprite.Bound.Y + (sprite.Bound.Height / 2f));
        }

        private static void LoadShapeDefault(Sprite sprite)
        {
            sprite.Shape = new Shape(Shape.Fill.Rectangle, sprite.Bound.Width, sprite.Bound.Height);
        }
    }
}
namespace UnderwaterGame.Worlds.Generation
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;

    public class SurfaceCaveGeneration : WorldGeneration
    {
        public Vector2[] cavePositions = new Vector2[2];
        
        public override void Generate()
        {
            return;
            int cavePositionsGap = 64;
            for(int i = 0; i < cavePositions.Length; i++)
            {
                int caveRadius = 8;
                float caveDirection = MathHelper.Pi / 2f;
                float caveDirectionAcc = MathHelper.Pi / 32f;
                float caveDirectionTo = 0f;
                int caveTime = 0;
                int caveTimeChange = 30;
                int caveTimeMax = 150;
                do
                {
                    cavePositions[i].X = Main.random.Next(World.width);
                    cavePositions[i].Y = 0;
                    while(World.GetTileAt((int)cavePositions[i].X, (int)cavePositions[i].Y, World.Tilemap.Solids) == null)
                    {
                        cavePositions[i].Y++;
                    }
                } while(!ValidCavePosition());
                while(caveTime < caveTimeMax)
                {
                    int xStart = (int)cavePositions[i].X - caveRadius;
                    int yStart = (int)cavePositions[i].Y - caveRadius;
                    int xEnd = (int)cavePositions[i].X + caveRadius;
                    int yEnd = (int)cavePositions[i].Y + caveRadius;
                    for(int y = yStart; y <= yEnd; y++)
                    {
                        for(int x = xStart; x <= xEnd; x++)
                        {
                            if(Vector2.Distance(cavePositions[i], new Vector2(x, y)) <= caveRadius)
                            {
                                World.RemoveTileAt(x, y, World.Tilemap.Solids);
                            }
                        }
                    }
                    cavePositions[i].X += (float)Math.Cos(caveDirection);
                    cavePositions[i].Y += (float)Math.Sin(caveDirection);
                    if(caveTime % caveTimeChange == 0)
                    {
                        caveDirectionTo = MathHelper.Pi * RandomUtilities.Range(0.25f, 0.75f);
                    }
                    float directionDifference = MathUtilities.AngleDifference(caveDirection, caveDirectionTo);
                    caveDirection += Math.Min(caveDirectionAcc, Math.Abs(directionDifference)) * Math.Sign(directionDifference);
                    caveTime++;
                }
                bool ValidCavePosition()
                {
                    SurfaceSpawnGeneration surfaceSpawnGeneration = (SurfaceSpawnGeneration)World.generations.Find((WorldGeneration worldGeneration) => worldGeneration is SurfaceSpawnGeneration);
                    if((cavePositions[i].X >= (World.width - surfaceSpawnGeneration.width - caveRadius) / 2) && (cavePositions[i].X <= (World.width + surfaceSpawnGeneration.width + caveRadius) / 2))
                    {
                        return false;
                    }
                    for(int ii = i - 1; ii >= 0; ii--)
                    {
                        if(Math.Abs(cavePositions[ii].X - cavePositions[i].X) < cavePositionsGap)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}
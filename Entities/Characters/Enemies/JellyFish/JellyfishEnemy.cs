﻿namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class JellyfishEnemy : EnemyCharacter
    {
        protected int swimTime;

        protected int swimTimeMax = 20;

        protected int swimBreakTime;

        protected int swimBreakTimeMax = 60;

        protected float swimSpeed;

        protected float swimSpeedAcc = 0.1f;

        protected float swimSpeedMax = 2f;

        protected float swimDirection;

        protected bool swimTargeted;

        protected void JellyfishUpdate()
        {
            if(swimTime > 0)
            {
                if(swimSpeed < swimSpeedMax)
                {
                    swimSpeed += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeed);
                }
                swimTime--;
            }
            else
            {
                if(swimSpeed > 0f)
                {
                    swimSpeed -= Math.Min(swimSpeedAcc, swimSpeed);
                }
                if(swimBreakTime < swimBreakTimeMax)
                {
                    swimBreakTime++;
                }
                else
                {
                    Vector2 swimPosition;
                    do
                    {
                        swimDirection = MathHelper.ToRadians(Main.random.Next(360));
                        swimPosition = position + MathUtilities.LengthDirection(swimSpeedMax * swimTimeMax, swimDirection);
                    } while(TileCollisionLine(position, swimPosition, World.Tilemap.Solids) || swimPosition.X < 0f || swimPosition.Y < 0f || position.X > World.width * Tile.size || position.Y > World.height * Tile.size);
                    swimTime = swimTimeMax;
                    swimBreakTime = 0;
                }
            }
            velocity = MathUtilities.LengthDirection(swimSpeed, swimDirection);
        }
    }
}
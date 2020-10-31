namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class JellyfishEnemy : EnemyCharacter
    {
        protected Vector2 swimPositionTo;

        protected bool swimPositionToReached = true;

        protected int swimPositionToTime;

        protected int swimPositionToTimeMax = 60 * 2;

        protected float swimSpeed;

        protected float swimSpeedAcc = 0.1f;

        protected float swimSpeedMax = 2f;

        protected float swimDirection;

        protected bool swimTargeted;

        protected void JellyfishUpdate()
        {
            if(swimPositionToTime > 0)
            {
                swimPositionToTime--;
            }
            else
            {
                do
                {
                    swimPositionTo = position + MathUtilities.LengthDirection(RandomUtilities.Range(40f, 60f), MathHelper.ToRadians(Main.random.Next(360)));
                } while(TileCollisionLine(position, swimPositionTo, World.Tilemap.Solids) || !InWorld(swimPositionTo));
                swimPositionToTime = swimPositionToTimeMax;
                swimPositionToReached = false;
            }
            float distanceTo = Vector2.Distance(position, swimPositionTo);
            if(distanceTo <= 20f)
            {
                swimPositionToReached = true;
            }
            if(!swimPositionToReached && inWater)
            {
                swimDirection = MathUtilities.PointDirection(position, swimPositionTo);
                if(swimSpeed < swimSpeedMax)
                {
                    swimSpeed += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeed);
                }
            }
            else
            {
                if(swimSpeed > 0f)
                {
                    swimSpeed -= Math.Min(swimSpeedAcc, swimSpeed);
                }
            }
            velocity = MathUtilities.LengthDirection(swimSpeed, swimDirection);
        }
    }
}
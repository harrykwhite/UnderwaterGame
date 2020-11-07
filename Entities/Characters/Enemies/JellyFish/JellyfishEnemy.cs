namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;

    public abstract class JellyfishEnemy : EnemyCharacter
    {
        protected int swimTime;

        protected int swimTimeMax = 30;

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
                    swimDirection = MathHelper.ToRadians(Main.random.Next(360));
                    swimTime = swimTimeMax;
                    swimBreakTime = 0;
                }
            }
            velocity = MathUtilities.LengthDirection(swimSpeed, swimDirection);
        }
    }
}
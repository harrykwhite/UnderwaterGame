namespace UnderwaterGame.Entities.Characters.Enemies.Sharks
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class SharkEnemy : EnemyCharacter
    {
        protected float swimSpeed;

        protected float swimSpeedAcc = 0.1f;
        
        protected float swimSpeedMax = 1f;
        
        protected float? swimDirection;

        protected float swimDirectionAcc = MathHelper.Pi / 108f;

        protected float swimDirectionTo;

        protected void SharkUpdate()
        {
            if(swimSpeed < swimSpeedMax)
            {
                swimSpeed += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeed);
            }
            swimDirectionTo = Vector2.Distance(position, hotspot.position) > Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f ? MathUtilities.PointDirection(position, hotspot.position) : MathUtilities.PointDirection(position, World.player.position);
            if (swimDirection == null)
            {
                swimDirection = swimDirectionTo;
            }
            float difference = MathUtilities.AngleDifference(swimDirection.Value, swimDirectionTo);
            swimDirection += Math.Min(swimDirectionAcc, Math.Abs(difference)) * Math.Sign(difference);
            velocity = MathUtilities.LengthDirection(swimSpeed, swimDirection.Value);
            flipHor = MathUtilities.AngleLeftHalf(swimDirection.Value);
        }
    }
}
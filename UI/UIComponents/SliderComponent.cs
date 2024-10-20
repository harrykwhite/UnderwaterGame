﻿namespace UnderwaterGame.Ui.UiComponents
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Options;
    using UnderwaterGame.Utilities;

    public class SliderComponent : UiComponent
    {
        private Vector2 ballPosition;

        private Vector2 ballScale = Vector2.One;

        private Vector2 ballScaleTo = Vector2.One;

        private bool ballSelected;

        public Option option;

        public float textGap = 32f;

        public static SliderComponent locked;

        public override void Draw()
        {
            Main.spriteBatch.Draw(Main.textureLibrary.UI_SLIDER_BAR.asset, getPosition() + new Vector2(0f, textGap / 2f), null, Color.White * 0.5f * getAlpha(), 0f, new Vector2(Main.textureLibrary.UI_SLIDER_BAR.asset.Width, Main.textureLibrary.UI_SLIDER_BAR.asset.Height) / 2f, scale * UiManager.scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(Main.textureLibrary.UI_SLIDER_BALL.asset, ballPosition + new Vector2(0f, textGap / 2f), null, Color.White * getAlpha(), 0f, new Vector2(Main.textureLibrary.UI_SLIDER_BALL.asset.Width, Main.textureLibrary.UI_SLIDER_BALL.asset.Height) / 2f, scale * ballScale * UiManager.scale, SpriteEffects.None, 1f);
            string optionText = option.name;
            string optionTextValue = option.value.ToString();
            switch(option.valueFormat)
            {
                case Option.Format.Percent:
                    optionTextValue = Math.Floor(option.value * 100f).ToString() + "%";
                    break;

                case Option.Format.Toggle:
                    optionTextValue = option.GetToggle() ? "Enabled" : "Disabled";
                    break;
            }
            DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(option.name + ": " + optionTextValue), getPosition() - new Vector2(0f, textGap / 2f), Color.White * getAlpha(), DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            bool ballSelectedPrevious = ballSelected;
            ballSelected = locked == this;
            if(getAlpha() > 0f && UiManager.menuCurrent == menuElement)
            {
                if(Vector2.Distance(ballPosition + new Vector2(0f, textGap / 2f), Control.GetMousePosition()) <= (Main.textureLibrary.UI_SLIDER_BALL.asset.Width + Main.textureLibrary.UI_OTHER_CURSOR.asset.Width) * 0.5f * UiManager.scale)
                {
                    if(locked == null)
                    {
                        ballSelected = true;
                        if(!ballSelectedPrevious)
                        {
                            SoundUtilities.PlaySound(Main.soundLibrary.UI_HOVER.asset);
                        }
                    }
                    if(Control.MouseLeftPressed())
                    {
                        if(locked != this)
                        {
                            locked = this;
                            SoundUtilities.PlaySound(Main.soundLibrary.UI_CLICK.asset);
                        }
                    }
                }
            }
            UpdateScale();
            ballScaleTo = ballSelected ? scaleMax : Vector2.One;
            ballScale += (ballScaleTo - ballScale) * scaleSpeed;
            if(locked == this)
            {
                if(getAlpha() <= 0f || !Control.MouseLeftHeld())
                {
                    locked = null;
                }
            }
            UpdateBall();
        }

        private void UpdateBall()
        {
            int width = (Main.textureLibrary.UI_SLIDER_BAR.asset.Width - Main.textureLibrary.UI_SLIDER_BALL.asset.Width) * UiManager.scale;
            float rawValue = option.value - option.valueMin;
            float rawSpan = option.valueMax - option.valueMin;
            if(locked == this)
            {
                float mouse = MathHelper.Clamp(Control.GetMousePosition().X, getPosition().X - (width / 2f), getPosition().X + (width / 2f));
                float valuePrevious = option.value;
                option.value = ((mouse - (getPosition().X - (width / 2f))) / width) * rawSpan;
                option.value += option.valueMin;
                if(option.valueRounded)
                {
                    option.value = (float)Math.Round(option.value);
                }
                int valueDisplay = (int)Math.Floor(option.value * 100f);
                int valuePreviousDisplay = (int)Math.Floor(valuePrevious * 100f);
                if(valueDisplay != valuePreviousDisplay)
                {
                    SoundUtilities.PlaySound(Main.soundLibrary.UI_SCROLL.asset);
                    if(option.valueRounded)
                    {
                        locked = null;
                    }
                }
            }
            ballPosition = getPosition() + new Vector2(((rawValue / rawSpan) - 0.5f) * width, 0f);
        }
    }
}
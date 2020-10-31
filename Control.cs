namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using UnderwaterGame.Ui;

    public static class Control
    {
        public static KeyboardState keyboardState;

        public static KeyboardState previousKeyboardState;

        public static MouseState mouseState;

        public static MouseState previousMouseState;

        public static void Update()
        {
        }

        public static void Refresh()
        {
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && !previousKeyboardState.IsKeyUp(key);
        }

        public static bool KeyHeld(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool MouseLeftPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed;
        }

        public static bool MouseMiddlePressed()
        {
            return mouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton != ButtonState.Pressed;
        }

        public static bool MouseRightPressed()
        {
            return mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed;
        }

        public static bool MouseLeftReleased()
        {
            return mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton != ButtonState.Released;
        }

        public static bool MouseMiddleReleased()
        {
            return mouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton != ButtonState.Released;
        }

        public static bool MouseRightReleased()
        {
            return mouseState.RightButton == ButtonState.Released && previousMouseState.RightButton != ButtonState.Released;
        }

        public static bool MouseLeftHeld()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool MouseMiddleHeld()
        {
            return mouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool MouseRightHeld()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool MouseScrollUp()
        {
            return mouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue;
        }

        public static bool MouseScrollDown()
        {
            return mouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue;
        }

        public static Vector2 GetMousePosition()
        {
            return mouseState.Position.ToVector2();
        }

        public static Vector2 GetMousePositionWorld()
        {
            return (GetMousePosition() / Camera.scale) + Camera.GetShape().position;
        }

        public static Vector2 GetMousePositionUi()
        {
            return GetMousePosition() / UiManager.scale;
        }
    }
}
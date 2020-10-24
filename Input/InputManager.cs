using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UnderwaterGame.UI;

namespace UnderwaterGame.Input
{
    public static class InputManager
    {
        public static KeyboardState KeyboardState { get; private set; }
        public static KeyboardState PreviousKeyboardState { get; private set; }

        public static MouseState MouseState { get; private set; }
        public static MouseState PreviousMouseState { get; private set; }

        private static bool CanInput => Main.loading == null;

        public static void Update()
        {

        }

        public static void Refresh()
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();
        }

        public static bool KeyPressed(Keys key) => CanInput && KeyboardState.IsKeyDown(key) && !PreviousKeyboardState.IsKeyDown(key);
        public static bool KeyReleased(Keys key) => CanInput && KeyboardState.IsKeyUp(key) && !PreviousKeyboardState.IsKeyUp(key);
        public static bool KeyHeld(Keys key) => CanInput && KeyboardState.IsKeyDown(key);

        public static bool MouseLeftPressed() => CanInput && MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton != ButtonState.Pressed;
        public static bool MouseMiddlePressed() => CanInput && MouseState.MiddleButton == ButtonState.Pressed && PreviousMouseState.MiddleButton != ButtonState.Pressed;
        public static bool MouseRightPressed() => CanInput && MouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton != ButtonState.Pressed;

        public static bool MouseLeftReleased() => CanInput && MouseState.LeftButton == ButtonState.Released && PreviousMouseState.LeftButton != ButtonState.Released;
        public static bool MouseMiddleReleased() => CanInput && MouseState.MiddleButton == ButtonState.Released && PreviousMouseState.MiddleButton != ButtonState.Released;
        public static bool MouseRightReleased() => CanInput && MouseState.RightButton == ButtonState.Released && PreviousMouseState.RightButton != ButtonState.Released;

        public static bool MouseLeftHeld() => CanInput && MouseState.LeftButton == ButtonState.Pressed;
        public static bool MouseMiddleHeld() => CanInput && MouseState.MiddleButton == ButtonState.Pressed;
        public static bool MouseRightHeld() => CanInput && MouseState.RightButton == ButtonState.Pressed;

        public static bool MouseScrollUp() => CanInput && MouseState.ScrollWheelValue > PreviousMouseState.ScrollWheelValue;
        public static bool MouseScrollDown() => CanInput && MouseState.ScrollWheelValue < PreviousMouseState.ScrollWheelValue;

        public static Vector2 GetMousePosition()
        {
            return MouseState.Position.ToVector2();
        }

        public static Vector2 GetMousePositionWorld()
        {
            return (GetMousePosition() / Camera.Scale) + Camera.Shape.position;
        }

        public static Vector2 GetMousePositionUI()
        {
            return GetMousePosition() / UIManager.Scale;
        }

        public static string ReadInput(string text, int length = -1)
        {
            Keys[] keys = KeyboardState.GetPressedKeys();

            foreach (Keys key in keys)
            {
                bool updateText = KeyPressed(key);

                if (updateText)
                {
                    if (key == Keys.Back)
                    {
                        if (text.Length > 0)
                        {
                            text = text.Remove(text.Length - 1, 1);
                        }
                    }
                    else
                    {
                        if (text.Length < length || length == -1)
                        {
                            text += InputToCharacter(key);
                        }
                    }
                }
            }

            return text;
        }

        public static string InputToCharacter(Keys key)
        {
            bool keyShift = KeyboardState.IsKeyDown(Keys.LeftShift) || KeyboardState.IsKeyDown(Keys.RightShift);

            switch (key)
            {
                case Keys.Space: return " ";

                case Keys.D0: return "0";
                case Keys.D1: return "1";
                case Keys.D2: return "2";
                case Keys.D3: return "3";
                case Keys.D4: return "4";
                case Keys.D5: return "5";
                case Keys.D6: return "6";
                case Keys.D7: return "7";
                case Keys.D8: return "8";
                case Keys.D9: return "9";

                case Keys.A: return keyShift ? "A" : "a";
                case Keys.B: return keyShift ? "B" : "b";
                case Keys.C: return keyShift ? "C" : "c";
                case Keys.D: return keyShift ? "D" : "d";
                case Keys.E: return keyShift ? "E" : "e";
                case Keys.F: return keyShift ? "F" : "f";
                case Keys.G: return keyShift ? "G" : "g";
                case Keys.H: return keyShift ? "H" : "h";
                case Keys.I: return keyShift ? "I" : "i";
                case Keys.J: return keyShift ? "J" : "j";
                case Keys.K: return keyShift ? "K" : "k";
                case Keys.L: return keyShift ? "L" : "l";
                case Keys.M: return keyShift ? "M" : "m";
                case Keys.N: return keyShift ? "N" : "n";
                case Keys.O: return keyShift ? "O" : "o";
                case Keys.P: return keyShift ? "P" : "p";
                case Keys.Q: return keyShift ? "Q" : "q";
                case Keys.R: return keyShift ? "R" : "r";
                case Keys.S: return keyShift ? "S" : "s";
                case Keys.T: return keyShift ? "T" : "t";
                case Keys.U: return keyShift ? "U" : "u";
                case Keys.V: return keyShift ? "V" : "v";
                case Keys.W: return keyShift ? "W" : "w";
                case Keys.X: return keyShift ? "X" : "x";
                case Keys.Y: return keyShift ? "Y" : "y";
                case Keys.Z: return keyShift ? "Z" : "z";
            }

            return "";
        }

        public static int InputToNumber(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) ? key - Keys.D0 : -1;
        }
    }
}
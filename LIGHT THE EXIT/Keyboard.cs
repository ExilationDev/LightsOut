using Microsoft.Xna.Framework.Input;

namespace LightTheExit
{
    class Keyboard
    {
        KeyboardState now, prev;

        public void update()
        {
            prev = now;
            now = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public bool isDown(Keys k) => now.IsKeyDown(k);

        public bool isPressed(Keys k)
        {
            return now.IsKeyDown(k) && prev.IsKeyUp(k);
        }
    }
}

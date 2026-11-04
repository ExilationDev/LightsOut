using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LightTheExit
{
    internal class Sprite
    {
        public Texture2D tex;
        public Vector2 position;
        public float scale = 0.5f;
        public bool flip;

        public Sprite(Texture2D t, Vector2 p)
        {
            tex = t;
            position = p;
        }

        public virtual void Update(GameTime gameTime, Keyboard kb)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 o = new Vector2(tex.Width / 2, tex.Height / 2);

            if (flip)
                sb.Draw(tex, position, null, Color.White, 0, o, scale, SpriteEffects.FlipHorizontally, 0);
            else
                sb.Draw(tex, position, null, Color.White, 0, o, scale, SpriteEffects.None, 0);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LightTheExit
{
    internal class Player : Sprite
    {
        public float spd = 300f;

        public Player(Texture2D t, Vector2 p) : base(t, p)
        {
        }

        public override void Update(GameTime gameTime, Keyboard kb)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 mv = Vector2.Zero;

            if (kb.isDown(Keys.W) || kb.isDown(Keys.Up)) mv.Y -= 1;
            if (kb.isDown(Keys.S) || kb.isDown(Keys.Down)) mv.Y += 1;
            if (kb.isDown(Keys.A) || kb.isDown(Keys.Left)) mv.X -= 1;
            if (kb.isDown(Keys.D) || kb.isDown(Keys.Right)) mv.X += 1;

            if (mv != Vector2.Zero)
            {
                mv.Normalize();
                position = position + mv * spd * dt;

                if (position.X < 0) position.X = 0;
                if (position.X > 3200) position.X = 3200;
                if (position.Y < 0) position.Y = 0;
                if (position.Y > 1800) position.Y = 1800;

                if (mv.X < 0) flip = true;
                if (mv.X > 0) flip = false;
            }

            base.Update(gameTime, kb);
        }
    }
}

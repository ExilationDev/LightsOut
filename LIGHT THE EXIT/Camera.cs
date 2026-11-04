using Microsoft.Xna.Framework;

namespace LightTheExit
{
    class Camera
    {
        public Vector2 pos;

        public void update(Vector2 target)
        {
            pos = new Vector2(target.X - 800, target.Y - 450);

            if (pos.X < 0) pos.X = 0;
            if (pos.X > 1600) pos.X = 1600;
            if (pos.Y < 0) pos.Y = 0;
            if (pos.Y > 900) pos.Y = 900;
        }

        public Matrix getMatrix()
        {
            return Matrix.CreateTranslation(-pos.X, -pos.Y, 0);
        }
    }
}

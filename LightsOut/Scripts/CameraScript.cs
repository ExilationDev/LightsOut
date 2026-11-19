using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace LightsOut.Scripts;

class CameraScript
{
    public static CameraScript camera { get; set; }

    private int _scrollValue = 1000;
    private float _clampedScrollValue;
    private int _minScrollValue = -1000;
    private int _maxScrollValue = 1000;

    private Matrix currentViewMatrix { get; set; }

    private float _camSpeed = 25;
    public readonly Viewport _viewport;

    public Vector2 position { get; set; }

    public float zoom = 1.0f;
    private Vector2 origin { get; set; }

    public CameraScript(Viewport viewport)
    {
        _viewport = viewport;
        this.position = Vector2.Zero;
        origin = new Vector2(_viewport.Width / 2f, _viewport.Height / 2f);
        camera = this;
    }

    Matrix GetViewMatrix()
    {
        return
            Matrix.CreateTranslation(new Vector3(-this.position, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
            Matrix.CreateRotationZ(0) *
            Matrix.CreateScale(zoom, zoom, 1) *
            Matrix.CreateTranslation(new Vector3(origin, 0.0f));
    }

    public void Update(GameTime gameTime)
    {
        // TODO: make the camera move
        currentViewMatrix = GetViewMatrix();
    }
}

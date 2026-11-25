using LightsOut.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGameLibrary;

namespace LightsOut;

public class Game1 : Core
{
    private Player player;
    // private CameraScript cameraScript;
    private OrthographicCamera camera;

    public Game1() : base("Lights Out", 1920, 1080, false) {
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        player = new Player();
        base.Initialize();

        BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1280, 720);
        camera = new OrthographicCamera(viewportAdapter);
    }

    protected override void LoadContent()
    {
        player.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Update(gameTime);

        float t = MathHelper.Clamp(10f * dt, 0f, 1f);
        // camera.LookAt(player.PlayerPosition);
        camera.LookAt(Vector2.Lerp(camera.Center, player.PlayerPosition, t));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());
        {
            player.Draw(SpriteBatch);
        }
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}

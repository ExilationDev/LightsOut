using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using MonoGameLibrary;

namespace LightsOut.Scripts.Screens;

public class GameplayScreen : GameScreen
{
    private Player player;
    private OrthographicCamera camera;

    public GameplayScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        player = new Player();
        base.Initialize();

        BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 640, 360);
        camera = new OrthographicCamera(viewportAdapter);
    }

    public override void LoadContent()
    {
        player.LoadContent(Content);
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Update(gameTime);

        float t = MathHelper.Clamp(10f * dt, 0f, 1f);
        camera.LookAt(Vector2.Lerp(camera.Center, player.PlayerPosition, t));
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());
        {
            player.Draw(Core.SpriteBatch);
        }
        Core.SpriteBatch.End();
    }
}

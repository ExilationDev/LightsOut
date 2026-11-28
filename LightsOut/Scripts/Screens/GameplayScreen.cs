using System;
using LightsOut.Scripts.WorldGen;
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
    Vector2 lerpCamPos;
    WorldGeneration worldGeneration;

    public GameplayScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        player = new Player();
        worldGeneration = new WorldGeneration();
        base.Initialize();

        // BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 640, 360);
        BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 4000, 2000); // temporary
        camera = new OrthographicCamera(viewportAdapter);
    }

    public override void LoadContent()
    {
        player.LoadContent(Content);
        worldGeneration.LoadContent(Content);
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // delta time
        player.Update(gameTime);

        float t = 1f - MathF.Exp(-10f * dt); // time
        // camera.LookAt(player.PlayerPosition);
        // camera.LookAt(Vector2.Lerp(camera.Center, player.PlayerPosition, t));
        Vector2 targetTopLeft = player.PlayerPosition - new Vector2(
            camera.BoundingRectangle.Width / 2f,
            camera.BoundingRectangle.Height / 2f
        );
        lerpCamPos = Vector2.Lerp(lerpCamPos, targetTopLeft, t);

        camera.Position = new Vector2(
            MathF.Round(lerpCamPos.X),
            MathF.Round(lerpCamPos.Y)
        );
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());
        {
            worldGeneration.Draw(Core.SpriteBatch, camera);
            player.Draw(Core.SpriteBatch);
        }
        Core.SpriteBatch.End();
    }
}

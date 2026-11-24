using LightsOut.Scripts;
using LightsOut.Scripts.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGame.Extended;

namespace LightsOut;

public class Game1 : Core
{
    private PlayerScript playerScript;
    private CameraScript cameraScript;

    public Game1() : base("Lights Out", 1920, 1080, false) {}

    protected override void Initialize()
    {
        playerScript = new PlayerScript();
        base.Initialize();
        cameraScript = new CameraScript(GraphicsDevice.Viewport, new Vector2(0, 35));
    }

    protected override void LoadContent()
    {
        playerScript.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        playerScript.Update(gameTime);

        cameraScript.Follow(playerScript.PlayerPosition);

        playerScript.PlayerPosition = RandomPoint.GetRandomPointInCircle(100);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cameraScript.GetViewMatrix());
        {
            playerScript.Draw(SpriteBatch);
        }
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}

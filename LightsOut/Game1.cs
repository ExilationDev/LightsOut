using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using LightsOut.Scripts;

namespace LightsOut;

public class Game1 : Core
{
    private PlayerScript playerScript;
    private CameraScript cameraScript;
    public Game1() : base("Lights Out", 1280, 720, false) {}

    protected override void Initialize()
    {
        playerScript = new PlayerScript();
        base.Initialize();
        cameraScript = new CameraScript(GraphicsDevice.Viewport);
    }

    protected override void LoadContent()
    {
        playerScript.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        playerScript.Update(gameTime);
        cameraScript.Update(gameTime);

        // TODO: make this thing work
        cameraScript.position = playerScript.playerPosition;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        {
            playerScript.Draw(SpriteBatch);
        }
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}

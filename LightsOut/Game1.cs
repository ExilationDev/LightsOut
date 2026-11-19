using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using LightsOut.Scripts;

namespace LightsOut;

public class Game1 : Core
{
    private PlayerScript playerScript;

    public Game1() : base("Lights Out", 1280, 720, false) {}

    protected override void Initialize()
    {
        playerScript = new PlayerScript();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        playerScript.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        playerScript.Update(gameTime);

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

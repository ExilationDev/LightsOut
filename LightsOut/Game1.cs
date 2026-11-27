using LightsOut.Scripts.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGameLibrary;

namespace LightsOut;

public class Game1 : Core
{

    private ScreenManager screenManager;

    public Game1() : base("Lights Out", 1920, 1080, false)
    {
        Window.AllowUserResizing = true;

        screenManager = new ScreenManager();
        Components.Add(screenManager);
    }

    protected override void Initialize()
    {
        base.Initialize();
        screenManager.ShowScreen(new GameplayScreen(Instance));
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
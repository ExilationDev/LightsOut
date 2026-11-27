using Microsoft.Xna.Framework;
using MonoGameLibrary;
using Gum.Forms.Controls;

namespace LightsOut.Scripts.Screens;

public class MainMenuScreen : MonoGameGameScreenGum
{

    public MainMenuScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        GumUI.Initialize(Core.Instance, Gum.Forms.DefaultVisualsVersion.Newest);

        Button btn = new Button();
        btn.AddToRoot();
        btn.X = 50;
        btn.Y = 50;
        btn.Text = "Geann";

        Button btn2 = new Button();
        btn2.AddToRoot();
        btn2.X = 50;
        btn2.Y = 150;
        btn2.Text = "Shin";

        base.Initialize();
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Update(GameTime gameTime) {
        GumUI.Update(gameTime);
    }

    public override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        GumUI.Draw();
    }
}

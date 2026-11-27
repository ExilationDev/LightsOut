using Gum;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace LightsOut.Scripts.Screens;

public abstract class MonoGameGameScreenGum : GameScreen
{
    public GumService GumUI => GumService.Default;

    protected MonoGameGameScreenGum(Game game) : base(game) { }
}

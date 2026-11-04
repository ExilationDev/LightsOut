using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace LightTheExit.Scripts;

internal class PlayerScript
{
    MonoGameLibrary.Graphics.Sprite playerTexture;

    public PlayerScript() {}

    public void LoadContent(ContentManager content)
    {
        TextureAtlas atlas = TextureAtlas.FromFile(content, "sprites/player_atlas.xml");
        playerTexture = atlas.CreateSprite("player");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        playerTexture.Draw(spriteBatch, Vector2.Zero);
    }
}

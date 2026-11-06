using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace LightTheExit.Scripts;

internal class PlayerScript
{
    AnimatedSprite playerTexture;

    public PlayerScript() {}

    public void LoadContent(ContentManager content)
    {
        TextureAtlas atlas = TextureAtlas.FromFile(content, "sprites/player_atlas.xml");

        playerTexture = atlas.CreateAnimatedSprite("playeridlefront-animation");
        playerTexture.Scale = new Vector2(4, 4);
    }

    public void Update(GameTime gameTime)
    {
        playerTexture.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        playerTexture.Draw(spriteBatch, Vector2.Zero);
    }
}

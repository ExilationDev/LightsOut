using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace LightTheExit.Scripts;

public class PlayerScript
{
    private const float PLAYER_MOVEMENT_SPEED = 4.0f;

    private Vector2 playerPos;
    private AnimatedSprite playerTexture;

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

        CheckKeyboardInput();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        playerTexture.Draw(spriteBatch, playerPos);
    }

    private void CheckKeyboardInput()
    {
        float speed = PLAYER_MOVEMENT_SPEED;
         
        if (Core.Input.Keyboard.IsKeyDown(Keys.W) || Core.Input.Keyboard.IsKeyDown(Keys.Up))
        {
            playerPos.Y -= speed;
        }

        if (Core.Input.Keyboard.IsKeyDown(Keys.S) || Core.Input.Keyboard.IsKeyDown(Keys.Down))
        {
            playerPos.Y += speed;
        }

        if (Core.Input.Keyboard.IsKeyDown(Keys.A) || Core.Input.Keyboard.IsKeyDown(Keys.Left))
        {
            playerPos.X -= speed;
        }

        if (Core.Input.Keyboard.IsKeyDown(Keys.D) || Core.Input.Keyboard.IsKeyDown(Keys.Right))
        {
            playerPos.X += speed;
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace LightsOut.Scripts;

public class PlayerScript
{
    private const float PLAYER_MOVEMENT_SPEED = 4.0f;

    public Vector2 playerPosition;

    private Vector2 _inputMovement;
    private AnimatedSprite _playerTexture;

    public PlayerScript() {}

    public void LoadContent(ContentManager content)
    {
        TextureAtlas atlas = TextureAtlas.FromFile(content, "sprites/player_atlas.xml");

        _playerTexture = atlas.CreateAnimatedSprite("playeridlefront-animation");
        _playerTexture.Scale = new Vector2(4, 4);
    }

    public void Update(GameTime gameTime)
    {
        _playerTexture.Update(gameTime);

        CheckKeyboardInput();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _playerTexture.Draw(spriteBatch, playerPosition);
    }

    private void CheckKeyboardInput()
    {
        float speed = PLAYER_MOVEMENT_SPEED;

        _inputMovement = new Vector2(
            (Core.Input.Keyboard.IsKeyDown(Keys.A) || Core.Input.Keyboard.IsKeyDown(Keys.Left) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.D) || Core.Input.Keyboard.IsKeyDown(Keys.Right) ? 1 : 0),
            (Core.Input.Keyboard.IsKeyDown(Keys.W) || Core.Input.Keyboard.IsKeyDown(Keys.Up) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.S) || Core.Input.Keyboard.IsKeyDown(Keys.Down) ? 1 : 0)
        );

        playerPosition += NormalizeSafe(_inputMovement) * speed;
    }

    Vector2 NormalizeSafe(Vector2 pos)
    {
        // this is to prevent NaN values
        if (pos == Vector2.Zero) return Vector2.Zero;
        pos.Normalize();
        return pos;
    }
}

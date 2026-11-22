using System;
using System.Diagnostics;
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
    private Vector2 inputMovement;
    private Vector2 prevMovement;

    private AnimatedSprite currentSprite;
    private AnimatedSprite idleBack, idleSide, idleFront;
    private AnimatedSprite walkBack, walkSide, walkFront;

    public PlayerScript() { }

    public void LoadContent(ContentManager content)
    {
        idleBack = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_idle_back_anim");
        idleSide = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_idle_side_anim");
        idleFront = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_idle_front_anim");
        walkBack = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_walk_back_anim");
        walkSide = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_walk_side_anim");
        walkFront = LoadAnimation(content, "sprites/jessiee_atlas.xml", "p_walk_front_anim");

        currentSprite = idleBack;
    }


    private static AnimatedSprite LoadAnimation(ContentManager content, string atlasFile, string animationName)
    {
        TextureAtlas atlas = TextureAtlas.FromFile(content, atlasFile);
        AnimatedSprite sprite = atlas.CreateAnimatedSprite(animationName);
        sprite.Scale = Vector2.One * 4;
        sprite.CenterOrigin();
        return sprite;
    }

    public void Update(GameTime gameTime)
    {
        CheckKeyboardInput();
        currentSprite = ChooseAnimation();
        currentSprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        currentSprite.Draw(spriteBatch, playerPosition);
    }

    private AnimatedSprite ChooseAnimation()
    {
        if (inputMovement.Length() <= 0) {
            idleSide.Effects = prevMovement.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            return (prevMovement.X, prevMovement.Y) switch
            {
                (0, 1) => idleBack,
                (0, -1) => idleFront,
                _ => idleSide
            };
        }
        walkSide.Effects = inputMovement.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        return (inputMovement.X, inputMovement.Y) switch
        {
            (0, 1) => walkBack,
            (0, -1) => walkFront,
            _ => walkSide
        };
    }

    private void CheckKeyboardInput()
    {
        float speed = PLAYER_MOVEMENT_SPEED;

        if (inputMovement.Length() > 0) prevMovement = inputMovement;
        inputMovement = new Vector2(
            (Core.Input.Keyboard.IsKeyDown(Keys.A) || Core.Input.Keyboard.IsKeyDown(Keys.Left) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.D) || Core.Input.Keyboard.IsKeyDown(Keys.Right) ? 1 : 0),
            (Core.Input.Keyboard.IsKeyDown(Keys.W) || Core.Input.Keyboard.IsKeyDown(Keys.Up) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.S) || Core.Input.Keyboard.IsKeyDown(Keys.Down) ? 1 : 0)
        );

        playerPosition += NormalizeSafe(inputMovement) * speed;
    }

    Vector2 NormalizeSafe(Vector2 pos)
    {
        // this is to prevent NaN values
        if (pos == Vector2.Zero) return Vector2.Zero;
        pos.Normalize();
        return pos;
    }
}

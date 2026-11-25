using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Animations;
using MonoGameLibrary;

namespace LightsOut.Scripts;

public class PlayerScript
{
    private const float PLAYER_MOVEMENT_SPEED = 4.0f;

    private Vector2 playerPosition;
    private Vector2 inputMovement;
    private Vector2 prevMovement;

    public Vector2 PlayerPosition { get { return playerPosition; } set { playerPosition = value; } }

    private SpriteSheet spriteSheet;
    private AnimatedSprite playerSprite;
    private string currentAnimation;

    public PlayerScript() { }

    public void LoadContent(ContentManager content)
    {
        Texture2DAtlas atlas = content.Load<Texture2DAtlas>("sprites/data/jessiee");
        spriteSheet = new SpriteSheet("sprites/jessiee_spritesheet", atlas);

        DefineAnimations(content, spriteSheet);

        prevMovement = new Vector2(0, 1);
    }

    public void Update(GameTime gameTime)
    {
        CheckKeyboardInput();
        playerSprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int scale = 4;
        spriteBatch.Draw(playerSprite, playerPosition, 0, new Vector2(scale));
    }

    private void UpdateAnimation()
    {
        bool isMoving = inputMovement.LengthSquared() > 0;
        Vector2 dir = isMoving ? inputMovement : prevMovement;

        playerSprite.Effect = dir.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        string action = isMoving ? "walk" : "idle";
        string facing = dir.Y switch
        {
            > 0 => "back",
            < 0 => "front",
            _ => "side"
        };

        string nextAnimation = $"{action}_{facing}";
        if (currentAnimation != nextAnimation)
        {
            currentAnimation = nextAnimation;
            playerSprite.SetAnimation(nextAnimation);
        }
    }

    private void DefineAnimations(ContentManager content, SpriteSheet spriteSheet)
    {
        spriteSheet.DefineAnimation("idle_back", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_idle_back_001", TimeSpan.FromSeconds(0.4))
            .AddFrame("jessiee_idle_back_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_back_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_back_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_back_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_back_006", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_back_007", TimeSpan.FromSeconds(0.2));
        });

        spriteSheet.DefineAnimation("idle_side", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_idle_side_001", TimeSpan.FromSeconds(0.4))
            .AddFrame("jessiee_idle_side_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_side_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_side_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_side_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_side_006", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_side_007", TimeSpan.FromSeconds(0.2));
        });

        spriteSheet.DefineAnimation("idle_front", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_idle_front_001", TimeSpan.FromSeconds(0.4))
            .AddFrame("jessiee_idle_front_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_front_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_front_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_front_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_front_006", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_idle_front_007", TimeSpan.FromSeconds(0.2));
        });

        spriteSheet.DefineAnimation("walk_back", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_walk_back_001", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_back_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_back_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_back_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_back_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_back_006", TimeSpan.FromSeconds(0.2));
        });

        spriteSheet.DefineAnimation("walk_side", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_walk_side_001", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_side_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_side_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_side_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_side_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_side_006", TimeSpan.FromSeconds(0.2));
        });

        spriteSheet.DefineAnimation("walk_front", builder =>
        {
            builder.IsLooping(true)
            .AddFrame("jessiee_walk_front_001", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_front_002", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_front_003", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_front_004", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_front_005", TimeSpan.FromSeconds(0.2))
            .AddFrame("jessiee_walk_front_006", TimeSpan.FromSeconds(0.2));
        });

        playerSprite = new AnimatedSprite(spriteSheet, "idle_back");
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

        UpdateAnimation();
    }

    Vector2 NormalizeSafe(Vector2 pos)
    {
        // this is to prevent NaN values
        if (pos == Vector2.Zero) return Vector2.Zero;
        pos.Normalize();
        return pos;
    }
}

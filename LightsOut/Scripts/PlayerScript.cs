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
    private const float PLAYER_MOVEMENT_SPEED = 4.0f;   // frame

    private Vector2 playerPos;
    private Vector2 inputMovement;                    

    // i removed here: private AnimatedSprite playerTexture;

    private AnimatedSprite currentSprite;   // i add here for the na aha nag face
                                            // ang player sa cuurent idle 
                                            //but when standing still ga face siya
                                            //sa front
    private AnimatedSprite idleDown;        // i add here for down idle
    private AnimatedSprite idleUp;          // i add here for up idle
    private AnimatedSprite walkDown;        // i add here for walking idle down
    private AnimatedSprite walkUp;          // i add here for the walking idle up
    private AnimatedSprite idleSide;        // i add here for side

    private bool facingUp;                  // i add here for true or false condition

    public PlayerScript() { }

    public void LoadContent(ContentManager content)
    {
        
        //i add these for the sprites
        // sa reference for its akoa siyang gi initialized
        //then para ma connect pud gikan sa each xml file na gi generate nako from 
        // animation frame and load in through it para ma act

        idleDown = LoadAnimation(content, "sprites/player_idle_down.xml", "idle-down");   // i add here
        idleUp = LoadAnimation(content, "sprites/player_idle_up.xml", "idle-up");         // i add here
        walkDown = LoadAnimation(content, "sprites/player_walk_down.xml", "walk-down");   // i add here
        walkUp = LoadAnimation(content, "sprites/player_walk_up.xml", "walk-up");         // i add here
        idleSide = LoadAnimation(content, "sprites/player_idle_side.xml", "idle-side");   // i add here

        currentSprite = idleDown;   // i add here
    }

    
    private static AnimatedSprite LoadAnimation(ContentManager content, string atlasFile, string animationName)
    {
        // Reads the XML that lists the frames (x, y, width, height) of that
        // sheet
        TextureAtlas atlas = TextureAtlas.FromFile(content, atlasFile);
        // Finds the <Animation> with this name inside the XML and
        // builds a sprite from it
        AnimatedSprite sprite = atlas.CreateAnimatedSprite(animationName);
        // Draw 4x bigger: 16x32 pixels per frame
        // becomes 64x128 on screen
        sprite.Scale = new Vector2(4, 4);
        // return the data
        return sprite;
    }

    public void Update(GameTime gameTime)
    {
        // i removed here: playerTexture.Update(gameTime) so i can turn into here
        // because kay since playerTexture kay nag base sa gisent ni Devie Vinz
        // therefore akoa siyang gi replace for the sprite na gihimo ni Devie Shinz
        CheckKeyboardInput();                          // i changed here
        currentSprite = ChooseAnimation();             // i add here 
        currentSprite.Update(gameTime);                // i changed here 
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        currentSprite.Draw(spriteBatch, playerPos);    // i changed here: same call, new field name kay wa lang para maisa
    }

    // i add here for the proper way for my own ways lang hehe
    private AnimatedSprite ChooseAnimation()
    {
        // moving up
        if (inputMovement.Y < 0)
        {
            facingUp = true;
            return walkUp;                             
        }
        //moving down
        if (inputMovement.Y > 0)
        {
            facingUp = false;
            return walkDown;                           
        }
        // lol i learn ternary operator for like 1 minute
        if (inputMovement.X != 0)                      
        {
            idleSide.Effects = inputMovement.X < 0
                ? SpriteEffects.FlipHorizontally
                : SpriteEffects.None;                  
            return idleSide;
        }
        return facingUp ? idleUp : idleDown;           
    }

    private void CheckKeyboardInput()
    {
        float speed = PLAYER_MOVEMENT_SPEED;

        inputMovement = new Vector2(
            (Core.Input.Keyboard.IsKeyDown(Keys.A) || Core.Input.Keyboard.IsKeyDown(Keys.Left) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.D) || Core.Input.Keyboard.IsKeyDown(Keys.Right) ? 1 : 0),
            (Core.Input.Keyboard.IsKeyDown(Keys.W) || Core.Input.Keyboard.IsKeyDown(Keys.Up) ? -1 : 0) +
            (Core.Input.Keyboard.IsKeyDown(Keys.S) || Core.Input.Keyboard.IsKeyDown(Keys.Down) ? 1 : 0)
        );

        playerPos += NormalizeSafe(inputMovement) * speed;
    }

    Vector2 NormalizeSafe(Vector2 pos)
    {
        // this is to prevent NaN values
        if (pos == Vector2.Zero) return Vector2.Zero;
        pos.Normalize();
        return pos;
    }
}

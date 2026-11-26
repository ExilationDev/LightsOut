using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace LightsOut.Scripts;

public abstract class Entity
{
    protected float entitySpeed = 3;
    protected Vector2 entityPosition;

    protected SpriteSheet spriteSheet;
    protected AnimatedSprite animatedSprite;
    protected string currentAnimation;

    public abstract void LoadContent(ContentManager content);
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}

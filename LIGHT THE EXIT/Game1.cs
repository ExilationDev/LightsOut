using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using LightTheExit.Scripts;

namespace LightTheExit;

public class Game1 : Core
{
    //Player player;
    //List<Sprite> enemies = new List<Sprite>();
    //Random rnd = new Random();
    //Keyboard kb = new Keyboard();

    //Texture2D texPlayer;
    //Texture2D texEnemy;
    //Texture2D dark;

    //Camera cam = new Camera();

    private PlayerScript playerScript;

    public Game1() : base("Lights Out", 1280, 720, false) {}

    protected override void Initialize()
    {
        playerScript = new PlayerScript();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        playerScript.LoadContent(Content);
        //texPlayer = Content.Load<Texture2D>("ccc1edbf-3773-4c27-b3d6-beb59b49dcd8");
        //texEnemy = Content.Load<Texture2D>("244d8923-fe04-4759-a334-075b5e92b9bf");

        //player = new Player(texPlayer, new Vector2(1600, 900));
        //player.scale = 0.15f;

        //for (int i = 0; i < 12; i++)
        //{
        //    Vector2 p;
        //    while (true)
        //    {
        //        p = new Vector2(rnd.Next(60, 3200 - 60), rnd.Next(60, 1800 - 60));
        //        bool ok = true;
        //        if (Vector2.Distance(p, player.position) < 140) ok = false;
        //        foreach(var other in enemies)
        //        {
        //            if (Vector2.Distance(p, other.position) < 80) ok = false;
        //        }
        //        if (ok) break;
        //    }

        //    Sprite e = new Sprite(texEnemy, p);
        //    e.scale = 0.15f;
        //    enemies.Add(e);
        //}
        //Debug.WriteLine("spawned " + enemies.Count);

        //Color[] px = new Color[1024 * 1024];
        //for (int y = 0; y < 1024; y++)
        //{
        //    for (int x = 0; x < 1024; x++)
        //    {
        //        float d = (float)Math.Sqrt((x - 512) * (x - 512) + (y - 512) * (y - 512));

        //        if (d < 40) px[y * 1024 + x] = new Color(0, 0, 0, 0);
        //        else if (d < 50) px[y * 1024 + x] = new Color(0, 0, 0, (int)(255 * ((d - 40) / 10)));
        //        else px[y * 1024 + x] = Color.Black;
        //    }
        //}

        //dark = new Texture2D(GraphicsDevice, 1024, 1024);
        //dark.SetData(px);
    }

    protected override void Update(GameTime gameTime)
    {
        playerScript.Update(gameTime);
        //kb.update();

        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.isDown(Keys.Escape)) Exit();

        //if (kb.isPressed(Keys.Space))
        //{
        //    Sprite target = null;
        //    float best = 150;

        //    foreach(var e in enemies)
        //    {
        //        float d = Vector2.Distance(e.position, player.position);
        //        if (d <= best)
        //        {
        //            best = d;
        //            target = e;
        //        }
        //    }

        //    if (target != null)
        //    {
        //        enemies.Remove(target);
        //        Debug.WriteLine("kill, left: " + enemies.Count);
        //    }
        //}

        //player.Update(gameTime, kb);

        //foreach(var e in enemies)
        //{
        //    e.Update(gameTime, kb);
        //}

        //cam.update(player.position);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cam.getMatrix());
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        playerScript.Draw(SpriteBatch);

        //foreach(var e in enemies)
        //{
        //    e.Draw(SpriteBatch);
        //}
        //player.Draw(SpriteBatch);

        // SpriteBatch.Draw(dark, player.position - cam.pos, null, Color.White, 0, new Vector2(dark.Width / 2, dark.Height / 2), 4, SpriteEffects.None, 0);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tilemaps;
using MonoGameLibrary;

namespace LightsOut.Scripts.WorldGen;

class WorldGeneration
{
    DungeonGrid grid;
    DungeonGenerator generator;
    TilemapTileset tileSet;

    public WorldGeneration() {}

    public void LoadContent(ContentManager content)
    {
        tileSet = content.Load<TilemapTileset>("sprites/data/prototype");
        generator = new DungeonGenerator();
        grid = generator.Generate(100, 100, 2, 50, 10, 25);
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                int localId = (int)grid.GetTile(i, j);
                if (localId < 0) continue;

                Texture2DRegion region = new Texture2DRegion(tileSet.Texture, tileSet.GetTileRegion(localId));
                Vector2 worldPos = new Vector2(i * 16, j * 16);

                Core.SpriteBatch.Draw(region, worldPos, Color.White);
            }
        }
    }
}

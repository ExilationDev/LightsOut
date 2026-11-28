using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tilemaps;

namespace LightsOut.Scripts.WorldGen;

class WorldGeneration
{
    DungeonGrid grid;
    DungeonGenerator generator;
    TilemapTileset tileSet;

    public WorldGeneration() {}

    public void LoadContent(ContentManager content)
    {
        tileSet = content.Load<TilemapTileset>("sprites/data/dungeongen_tileset1");
        generator = new DungeonGenerator();
        grid = generator.Generate(350, 350, 50, 50, 3, 25);
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch, OrthographicCamera cam)
    {
        const int tileSize = 16;
        RectangleF view = cam.BoundingRectangle;

        int left = (int)MathF.Max(0, MathF.Floor(view.Left / tileSize));
        int right = (int)MathF.Min(grid.Width - 1, MathF.Floor(view.Right / tileSize));
        int top = (int)MathF.Max(0, MathF.Floor(view.Top / tileSize));
        int bottom = (int)MathF.Min(grid.Height - 1, MathF.Floor(view.Bottom / tileSize));

        for (int i = left; i <= right; i++)
        {
            for (int j = top; j <= bottom; j++)
            {
                int localId = (int)grid.GetTile(i, j);
                if (localId < 0) continue;

                Texture2DRegion region = new Texture2DRegion(tileSet.Texture, tileSet.GetTileRegion(localId));
                Vector2 worldPos = new Vector2(i * tileSize, j * tileSize);

                spriteBatch.Draw(region, worldPos, Color.White);
            }
        }

        generator.DrawTriangulate(spriteBatch, tileSize);
    }
}

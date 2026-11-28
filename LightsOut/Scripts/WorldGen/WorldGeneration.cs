using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tilemaps;
using MonoGameLibrary;

namespace LightsOut.Scripts.WorldGen;

class WorldGeneration
{
    DungeonGrid grid;
    DungeonGenerator generator;
    TilemapTileset tileSet;

    BasicEffect basicEffect;
    RasterizerState wireframeState;

    public WorldGeneration() {}

    public void LoadContent(ContentManager content)
    {
        basicEffect = new BasicEffect(Core.GraphicsDevice)
        {
            VertexColorEnabled = true,
            Projection = Matrix.CreateOrthographicOffCenter(
            0, Core.GraphicsDevice.Viewport.Width,
            Core.GraphicsDevice.Viewport.Height, 0,
            0, 1)
        };

        wireframeState = new RasterizerState { FillMode = FillMode.WireFrame };

        tileSet = content.Load<TilemapTileset>("sprites/data/dungeongen_tileset1");
        generator = new DungeonGenerator();
        grid = generator.Generate(200, 200, 2, 25, 10, 25);
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

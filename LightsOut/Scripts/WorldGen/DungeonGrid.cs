using Microsoft.Xna.Framework;

namespace LightsOut.Scripts.WorldGen;

public enum DungeonTile
{
    Void = 0,
    Room = 1,
    Path = 2
}

public class DungeonGrid
{
    private readonly DungeonTile[,] tiles;
    public int Width { get; }
    public int Height { get; }

    public DungeonGrid(int width, int height)
    {
        Width = width;
        Height = height;
        tiles = new DungeonTile[width, height];
    }

    public DungeonTile GetTile(int x, int y) => tiles[x, y];
    public void SetTile(int x, int y, DungeonTile tile) => tiles[x, y] = tile;
    public bool IsInBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
}

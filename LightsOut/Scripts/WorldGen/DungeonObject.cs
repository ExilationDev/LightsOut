using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace LightsOut.Scripts.WorldGen;

public struct DungeonObject
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; }
    public int Height { get; }

    public float CenterX => X + Width * 0.5f;
    public float CenterY => Y + Height * 0.5f;

    public int GridX => (int)MathF.Round(X);
    public int GridY => (int)MathF.Round(Y);
    public int GridRight => GridX + Width;
    public int GridBottom => GridY + Height;
    public int GridCenterX => GridX + Width / 2;
    public int GridCenterY => GridY + Height / 2;

    public DungeonObject(float x, float y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public bool Overlaps(DungeonObject other, float buffer = 1f)
    {
        return X - buffer < other.X + other.Width &&
               X + Width + buffer > other.X &&
               Y - buffer < other.Y + other.Height &&
               Y + Height + buffer > other.Y;
    }
}

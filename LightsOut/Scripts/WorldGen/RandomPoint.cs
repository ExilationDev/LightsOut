using System;
using System.Numerics;

namespace LightsOut.Scripts.WorldGen;

static class RandomPoint
{
    static int tileSize = 16;

    static float RoundSnap(float n, float m) => (float)Math.Floor((double)(n + m - 1) / m) * m;

    public static Vector2 GetRandomPointInCircle(int radius)
    {
        float t = 2 * (float)Math.PI * Random.Shared.NextSingle();
        float u = Random.Shared.NextSingle() * Random.Shared.NextSingle();
        float r = u > 1 ? 2 - u : u;
        return new Vector2(
            RoundSnap(radius * r * (float)Math.Cos(t), tileSize),
            RoundSnap(radius * r * (float)Math.Sin(t), tileSize)
        );
    }

    public static Vector2 GetRandomPointInEllipse(int ellipseWidth, int ellipseHeight)
    {
        float t = 2 * (float)Math.PI * Random.Shared.NextSingle();
        float u = Random.Shared.NextSingle() * Random.Shared.NextSingle();
        float r = u > 1 ? 2 - u : u;
        return new Vector2(
            RoundSnap(ellipseWidth * r * (float)Math.Cos(t), tileSize),
            RoundSnap(ellipseHeight * r * (float)Math.Sin(t), tileSize)
        );
    }
}

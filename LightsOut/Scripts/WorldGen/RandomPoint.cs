using System;
using System.Numerics;

namespace LightsOut.Scripts.WorldGen;

static class RandomPoint
{
    const int tileSize = 16;

    public static float RoundSnap(float value) => MathF.Round(value / tileSize) * tileSize;

    public static Vector2 GetRandomPointInCircle(float radius, Random rng)
    {
        float t = 2 * MathF.PI * rng.NextSingle();
        float u = rng.NextSingle() * rng.NextSingle();
        float r = u > 1 ? 2 - u : u;
        return new Vector2(
            RoundSnap(radius * r * MathF.Cos(t)),
            RoundSnap(radius * r * MathF.Sin(t))
        );
    }

    public static Vector2 GetRandomPointInEllipse(float ellipseWidth, float ellipseHeight, Random rng)
    {
        float t = 2 * MathF.PI * rng.NextSingle();
        float u = rng.NextSingle() * rng.NextSingle();
        float r = u > 1 ? 2 - u : u;
        return new Vector2(
            RoundSnap(ellipseWidth * r * MathF.Cos(t)),
            RoundSnap(ellipseHeight * r * MathF.Sin(t))
        );
    }
}

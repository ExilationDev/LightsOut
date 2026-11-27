using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace LightsOut.Scripts.WorldGen;

/// <summary>
/// The TKdev Dungeon Generator Algorithm
/// </summary>
class DungeonGenerator
{
    public DungeonGrid Generate(int width, int height, int seed, int roomCount = 25, int minRoomSize = 4, int maxRoomSize = 9)
    {
        var grid = new DungeonGrid(width, height);
        var rng = new Random(seed);

        List<DungeonObject> rooms = SpawnClusteredRooms(width, height, roomCount, minRoomSize, maxRoomSize, rng);

        SeparateRooms(rooms, rng);

        foreach (DungeonObject room in rooms)
        {
            if (IsInsideGrid(room, width, height))
            {
                CarveRoom(grid, room);
            }
        }

        return grid;
    }

    private List<DungeonObject> SpawnClusteredRooms(int gridWidth, int gridHeight, int count, int minSize, int maxSize, Random rng)
    {
        var rooms = new List<DungeonObject>(count);
        var gridCenter = new Vector2(gridWidth * 0.5f, gridHeight * 0.5f);
        float spawnRadius = MathF.Min(gridWidth, gridHeight) * 0.15f;

        for (int i = 0; i < count; i++)
        {
            int w = rng.Next(minSize, maxSize + 1);
            int h = rng.Next(minSize, maxSize + 1);

            Vector2 offset = RandomPoint.GetRandomPointInCircle(spawnRadius, rng);
            float x = gridCenter.X + offset.X - (w * 0.5f);
            float y = gridCenter.Y + offset.Y - (h * 0.5f);

            rooms.Add(new DungeonObject(x, y, w, h));
        }

        return rooms;
    }

    private bool IsInsideGrid(DungeonObject room, int gridWidth, int gridHeight)
    {
        return room.GridX >= 1 &&
               room.GridY >= 1 &&
               room.GridRight <= gridWidth - 1 &&
               room.GridBottom <= gridHeight - 1;
    }

    private void CarveRoom(DungeonGrid grid, DungeonObject room)
    {
        for (int y = room.GridY; y < room.GridBottom; y++)
        {
            for (int x = room.GridX; x < room.GridRight; x++)
            {
                grid.SetTile(x, y, DungeonTile.Room);
            }
        }
    }

    // Separation Steering
    private void SeparateRooms(List<DungeonObject> rooms, Random rng, float buffer = 1f, float stepSpeed = 0.5f, int maxIterations = 500)
    {
        for (int iter = 0; iter < maxIterations; iter++)
        {
            bool anyOverlap = false;
            Vector2[] forces = new Vector2[rooms.Count];

            for (int i = 0; i < rooms.Count; i++)
            {
                Vector2 steering = Vector2.Zero;
                int neighborCount = 0;

                for (int j = 0; j < rooms.Count; j++)
                {
                    if (i == j) continue;

                    if (rooms[i].Overlaps(rooms[j], buffer))
                    {
                        anyOverlap = true;
                        neighborCount++;

                        Vector2 away = new Vector2(
                            rooms[i].CenterX - rooms[j].CenterX,
                            rooms[i].CenterY - rooms[j].CenterY
                        );

                        if (away.LengthSquared() < 0.0001f)
                        {
                            double jitterAngle = rng.NextDouble() * Math.PI * 2.0;
                            away = new Vector2(MathF.Cos((float)jitterAngle), MathF.Sin((float)jitterAngle));
                        }
                        else
                        {
                            away = Vector2.Normalize(away);
                        }

                        steering += away;
                    }
                }

                if (neighborCount > 0)
                {
                    steering /= neighborCount;
                    if (steering.LengthSquared() > 0.0001f)
                        forces[i] = Vector2.Normalize(steering) * stepSpeed;
                }
            }

            if (!anyOverlap)
                break;

            for (int i = 0; i < rooms.Count; i++)
            {
                DungeonObject room = rooms[i];
                room.X += forces[i].X;
                room.Y += forces[i].Y;
                rooms[i] = room;
            }
        }
    }
}

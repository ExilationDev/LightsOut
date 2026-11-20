using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CameraScript
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 PositionOffset { get; set; } = Vector2.Zero;
    public float Zoom { get; set; } = 1f;
    public float Rotation { get; set; } = 0f;

    private readonly Viewport _viewport;

    public CameraScript(Viewport viewport)
    {
        _viewport = viewport;
    }

    public CameraScript(Viewport viewport, Vector2 positionOffset)
    {
        _viewport = viewport;
        PositionOffset = positionOffset;
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-Position.X - PositionOffset.X, -Position.Y - PositionOffset.Y, 0)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom, Zoom, 1f) *
               Matrix.CreateTranslation(new Vector3(_viewport.Width * 0.5f, _viewport.Height * 0.5f, 0));
    }

    public void Follow(Vector2 targetPosition, float lerpAmount = 0.15f)
    {
        Position = Vector2.Lerp(Position, targetPosition, lerpAmount);
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        return Vector2.Transform(screenPosition, Matrix.Invert(GetViewMatrix()));
    }

    public Vector2 WorldToScreen(Vector2 worldPosition)
    {
        return Vector2.Transform(worldPosition, GetViewMatrix());
    }
}
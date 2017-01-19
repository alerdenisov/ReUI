namespace ReUI.Api
{
    public enum PFlags
    {
        Top = 1 << 0,
        Bottom = 1 << 1,
        Middle = 1 << 2,

        Left = 1 << 3,
        Center = 1 << 4,
        Right = 1 << 5
    }

    public enum PivotType
    {
        Free,

        TopLeft = PFlags.Top | PFlags.Left,
        TopCenter = PFlags.Top | PFlags.Center,
        TopRight = PFlags.Top | PFlags.Right,

        MiddleLeft = PFlags.Middle | PFlags.Left,
        MiddleCenter = PFlags.Middle | PFlags.Center,
        MiddleRight = PFlags.Middle | PFlags.Right,

        BottomLeft = PFlags.Bottom | PFlags.Left,
        BottomCenter = PFlags.Bottom | PFlags.Center,
        BottomRight = PFlags.Bottom | PFlags.Right
    }
}
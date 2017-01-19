using ReUI.Implementation;
using Uniful;
using UnityEngine;

namespace ReUI.Api
{
    public static class PivotTypeVector2Converter
    {
        public static Vector2 Convert(PivotType pivotType)
        {
            var x = 0f;
            var y = 0f;

            if (pivotType.HasFlag(PFlags.Top))
                y = 1f;
            else if (pivotType.HasFlag(PFlags.Middle))
                y = 0.5f;

            if (pivotType.HasFlag(PFlags.Right))
                x = 1f;
            else if (pivotType.HasFlag(PFlags.Center))
                x = 0.5f;

            return new Vector2(x, y);
        }
    }
}
using ReUI.Implementation;
using UnityEngine;

namespace ReUI.Api
{
    public class AnchorTypeVectro4Converter
    {
        public static Vector4 Convert(AnchorType type)
        {
            var result = new Vector4();
            switch (type)
            {
                case AnchorType.TopLeft:
                case AnchorType.TopCenter:
                case AnchorType.TopRight:
                case AnchorType.TopStretch:
                    result.y = result.w = 1;
                    break;
                case AnchorType.BottomLeft:
                case AnchorType.BottomCenter:
                case AnchorType.BottomRight:
                case AnchorType.BottomStretch:
                    result.y = result.w = 0;
                    break;
                case AnchorType.MiddleLeft:
                case AnchorType.MiddleCenter:
                case AnchorType.MiddleRight:
                case AnchorType.MiddleStretch:
                    result.y = result.w = 0.5f;
                    break;
                case AnchorType.StretchLeft:
                case AnchorType.StretchCenter:
                case AnchorType.StretchRight:
                case AnchorType.Stretch:
                    result.y = 0;
                    result.w = 1;
                    break;
            }
            switch (type)
            {
                case AnchorType.TopLeft:
                case AnchorType.BottomLeft:
                case AnchorType.MiddleLeft:
                case AnchorType.StretchLeft:
                    result.x = result.z = 0;
                    break;
                case AnchorType.TopCenter:
                case AnchorType.MiddleCenter:
                case AnchorType.StretchCenter:
                case AnchorType.BottomCenter:
                    result.x = result.z = 0.5f;
                    break;
                case AnchorType.TopRight:
                case AnchorType.BottomRight:
                case AnchorType.MiddleRight:
                case AnchorType.StretchRight:
                    result.x = result.z = 1f;
                    break;
                case AnchorType.MiddleStretch:
                case AnchorType.BottomStretch:
                case AnchorType.TopStretch:
                case AnchorType.Stretch:
                    result.x = 0;
                    result.z = 1;
                    break;
            }

            return result;
        }
    }
}
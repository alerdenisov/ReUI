using UnityEngine;

namespace ReUI.Api
{
    public class ColorConverter
    {
        public static UnityEngine.Color Convert(string value)
        {
            UnityEngine.Color r = UnityEngine.Color.white;
            Convert(value, ref r);
            return r;
        }

        public static bool Convert(string value, ref UnityEngine.Color result)
        {
            long hex = 0;
            if (!HexConverter.Convert(value, ref hex))
                return false;

            var color32 = new Color32();
            var byteValues = new byte[4];
            byteValues[0] = (byte)((hex & 0xFF000000)   >> 24);
            byteValues[1] = (byte)((hex & 0xFF0000)     >> 16);
            byteValues[2] = (byte)((hex & 0xFF00)       >> 8);
            byteValues[3] = (byte)( hex & 0xFF);

            var start = 0;
            if (value.Length <= 8) start = 1;

            color32.r = byteValues[start++];
            color32.g = byteValues[start++];
            color32.b = byteValues[start++];
            color32.a = start < 4 ? byteValues[start] : byte.MaxValue;

            result = color32;

            return true;
        }
    }
}
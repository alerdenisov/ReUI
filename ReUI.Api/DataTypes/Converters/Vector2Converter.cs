using System.Globalization;
using System.Linq;
using UnityEngine;

namespace ReUI.Api
{
    public static class Vector2Converter
    {
        public static bool Convert(string value, ref Vector2 result)
        {
            float[] valueList;
            try
            {
                valueList = value.Split(',')
                    .Select(x => System.Convert.ToSingle(x, CultureInfo.InvariantCulture)).ToArray();
            }
            catch
            {
                return false;
            }

            switch (valueList.Length)
            {
                case 1:
                    result = new Vector2(valueList[0], valueList[0]);
                    return true;
                case 2:
                    result = new Vector2(valueList[0], valueList[1]);
                    return true;
                default:
                    return false;
            }
        }
    }
}
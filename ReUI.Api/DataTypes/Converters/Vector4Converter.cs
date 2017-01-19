using System.Globalization;
using System.Linq;
using UnityEngine;

namespace ReUI.Api
{
    public static class Vector4Converter
    {
        public static bool Convert(string value, ref Vector4 result)
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
                    result = new Vector4(valueList[0], valueList[0], valueList[0], valueList[0]);
                    return true;
                case 2:
                    result = new Vector4(valueList[0], valueList[1]);
                    return true;
                case 3:
                    result = new Vector4(valueList[0], valueList[1], valueList[2]);
                    return true;
                case 4:
                    result = new Vector4(valueList[0], valueList[1], valueList[2], valueList[3]);
                    return true;
                default:
                    return false;
            }
        }
    }
}
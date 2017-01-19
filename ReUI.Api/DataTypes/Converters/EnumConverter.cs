using System;

namespace ReUI.Api
{
    public class EnumConverter<T>
    {
        public static bool Convert(string value, ref T result)
        {
            if (!typeof (T).IsEnum)
                return false;

            try
            {
                result = (T) Enum.Parse(typeof (T), value, true);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
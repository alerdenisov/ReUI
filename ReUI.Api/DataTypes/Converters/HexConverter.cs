namespace ReUI.Api
{
    public static class HexConverter
    {
        public static bool Convert(string value, ref long result)
        {
            if (value.StartsWith("#"))
            {
                value = value.Substring(1);
            }
            else if (value.StartsWith("0x"))
            {
            }
            else return false;

            try
            {
                result = System.Convert.ToInt64(value, 16);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
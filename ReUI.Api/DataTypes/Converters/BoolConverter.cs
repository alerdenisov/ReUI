namespace ReUI.Api
{
    public class BoolConverter
    {
        public static bool Convert(string value, ref bool result)
        {
            bool boolValue;
            if (bool.TryParse(value, out boolValue))
            {
                result = boolValue;
                return true;
            }

            return false;
        }
    }
}
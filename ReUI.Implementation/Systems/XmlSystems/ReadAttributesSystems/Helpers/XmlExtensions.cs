using ReUI.Api;
using UnityEngine;
using Color = UnityEngine.Color;

namespace ReUI.Implementation.Helpers
{
    public static class XmlExtensions
    {
        public static bool HasAttribute(this XmlElement xml, string attribute)
        {
            return xml.Attributes.ContainsKey(attribute);
        }

        public static bool HasFloat(this XmlElement xml, string attribute, out float value)
        {
            value = 0f;
            if (!xml.HasAttribute(attribute))
                return false;

            return float.TryParse(xml.Attributes[attribute], out value);
        }

        public static bool HasInt(this XmlElement xml, string attribute, out int value)
        {
            value = 0;
            if (!xml.HasAttribute(attribute))
                return false;

            return int.TryParse(xml.Attributes[attribute], out value);
        }

        public static bool HasFlag(this XmlElement xml, string attribute)
        {
            if (!xml.HasAttribute(attribute))
                return false;

            var has = false;
            return BoolConverter.Convert(xml.Attributes[attribute], ref has) && has;
        }

        public static bool HasEnum<T>(this XmlElement xml, string attribute, out T value)
        {
            value = default(T);
            if (!typeof (T).IsEnum) return false;
            if (!xml.HasAttribute(attribute)) return false;

            return EnumConverter<T>.Convert(xml.Attributes[attribute], ref value);
        }

        public static bool HasVector2(this XmlElement xml, string attribute, out Vector2 value)
        {
            value = Vector2.zero;
            if (!xml.HasAttribute(attribute))
                return false;

            return Vector2Converter.Convert(xml.Attributes[attribute], ref value);
        }

        public static bool HasVector3(this XmlElement xml, string attribute, out Vector3 value)
        {
            value = Vector3.zero;
            if (!xml.HasAttribute(attribute))
                return false;

            return Vector3Converter.Convert(xml.Attributes[attribute], ref value);
        }

        public static bool HasHex(this XmlElement xml, string attribute, out long value)
        {
            value = 0;
            if (!xml.HasAttribute(attribute))
                return false;

            return HexConverter.Convert(xml.Attributes[attribute], ref value);
        }

        public static bool HasVector4(this XmlElement xml, string attribute, out Vector4 value)
        {
            value = Vector4.zero;
            if (!xml.HasAttribute(attribute))
                return false;

            return Vector4Converter.Convert(xml.Attributes[attribute], ref value);
        }

        public static bool HasColor(this XmlElement xml, string attribute, out Color value)
        {
            value = Color.white;
            if (!xml.HasAttribute(attribute))
                return false;

            Vector4 vecColor;
            if (xml.HasVector4("Color", out vecColor))
            {
                value = vecColor;
                return true;
            }

            return ColorConverter.Convert(xml.Attributes[attribute], ref value);
        }
    }
}
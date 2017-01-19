using Rentitas;
using UnityEngine;

namespace ReUI.Api
{

    public class Text : IAttributeValue<string>, ILabelComponent
    {
        public string Value { get; set; }
    }

    public class Font : IAttributeValue<string>, ILabelComponent
    {
        public string Value { get; set; }
    }

    public class FontSize : IAttributeValue<int>, ILabelComponent
    {
        public int Value { get; set; }
    }

    public class LineSpacing : IAttributeValue<float>, ILabelComponent
    {
        public float Value { get; set; }
    }

    public class FontStyle : IAttributeValue<UnityEngine.FontStyle>, ILabelComponent
    {
        public UnityEngine.FontStyle Value { get; set; }
    }

    public class TextAlignment : IAttributeValue<TextAnchor>, ILabelComponent
    {
        public TextAnchor Value { get; set; }
    }
}
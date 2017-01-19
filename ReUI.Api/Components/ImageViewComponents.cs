using System;
using Rentitas;
using UnityEngine;
using UnityEngine.UI;

namespace ReUI.Api
{
    public class Resource : IAttributeValue<string>, ITextureComponent
    {
        public string Value { get; set; }
    }

    public class Texture : IAttributeValue<UnityEngine.Texture>, ITextureComponent
    {
        public UnityEngine.Texture Value { get; set; }
    }

    public class Sprite : IAttributeValue<UnityEngine.Sprite>, ISpriteComponent
    {
        public UnityEngine.Sprite Value { get; set; }
    }

    public class SplitMode : IAttributeValue<Image.Type>, IImageComponent
    {
        public Image.Type Value { get; set; }
    }

    public class Material : IAttributeValue<UnityEngine.Material>, IImageComponent
    {
        public UnityEngine.Material Value { get; set; }
    }

    public class Color : IAttributeValue<UnityEngine.Color>, IImageComponent
    {
        public UnityEngine.Color Value { get; set; }
    }

    public class SpriteResourceReceiver : ISpriteComponent
    {
        public Guid ElementId;
        public Sprite Resource;
    }
}
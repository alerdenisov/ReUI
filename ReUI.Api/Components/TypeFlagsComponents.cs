using Rentitas;

namespace ReUI.Api
{
    /// <summary>
    /// Type of UI Element declations
    /// </summary>
    public enum Elements
    {
        Root,
        Embed,
        GameObject,
        Sprite,
        RawImage,
        Text,
        Loop,
        Hierarchy,
        Children,
        TextInput
    }

    /// <summary>
    /// Attribute: Element Type
    /// <seealso cref="IAttribute"/>
    /// <seealso cref="IAttributeValue{T}"/>
    /// </summary>
    public class ElementType : IAttributeValue<Elements>, IViewComponent
    {
        public Elements Value { get; set; }
    }

    /// <summary>
    /// Flag of root elements
    /// </summary>
    public class ScopeType : IScopeComponent, IFlag { }

    /// <summary>
    /// Flag of raw texture elements (Fit to RawImage element)
    /// </summary>
    public class TextureType : ITextureComponent, IFlag { }

    /// <summary>
    /// Flag os sprite elements (Fit to Image and Color elements)
    /// </summary>
    public class SpriteType : ISpriteComponent, IFlag { }

    /// <summary>
    /// Flag of text elements (Fit to Text element)
    /// </summary>
    public class TextType : ILabelComponent, IFlag { }

    /// <summary>
    /// Flag of elements contains lua logics
    /// </summary>
    public class LuaType : ILuaComponent, IFlag { }

    /// <summary>
    /// Flag of container for injection hierarchy scope
    /// </summary>
    public class ChildrenType : IViewComponent, IFlag { }

    /// <summary>
    /// Flag of container for injected hierarchy
    /// </summary>
    public class HierarchyType : IViewComponent, IFlag { }


    /// <summary>
    /// Flag of text input element
    /// </summary>
    public class TextInputType : IViewComponent, IFlag { }
}
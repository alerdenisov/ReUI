using System;
using UnityEngine.EventSystems;

namespace ReUI.Api
{
    /// <summary>
    /// XML components
    /// </summary>
    public interface IXmlComponents : IUIPool { }

    /// <summary>
    /// Reactive attribute component 
    /// </summary>
    public interface IAttribute : IUIPool { }

    /// <summary>
    /// Typed reactive attribute component
    /// </summary>
    /// <typeparam name="T">Type of attribute</typeparam>
    public interface IAttributeValue<T> : IAttribute { T Value { get; set; } }

    /// <summary>
    /// Components of element (non data)
    /// </summary>
    public interface IViewComponent : IUIPool { }

    /// <summary>
    /// Components of scope element (ui scope)
    /// </summary>
    public interface IScopeComponent : IUIPool { }

    /// <summary>
    /// Components of element lua behaviour
    /// </summary>
    public interface ILuaComponent : IViewComponent { }

    /// <summary>
    /// Components of raw (string) lua code 
    /// </summary>
    public interface ILuaCodeComponent : ILuaComponent { }

    /// <summary>
    /// Components of lua compiled data
    /// </summary>
    public interface ILuaCompiledComponent : ILuaComponent { }

    /// <summary>
    /// Components of element itterator 
    /// </summary>
    public interface ILoopComponents : IViewComponent { }

    /// <summary>
    /// Components of element with visual representation
    /// (View will have Graphic Unity component)
    /// </summary>
    public interface IGraphicComponent : IViewComponent { }

    /// <summary>
    /// Components of received pointer events
    /// </summary>
    public interface IPointerComponent : IGraphicComponent
    {
        Guid Id { get; set; }
        PointerEventData Data { get; set; }
    }

    /// <summary>
    /// Components of element which have image based representation (Sprite or RawTexture)
    /// </summary>
    public interface IImageComponent : IGraphicComponent { }

    /// <summary>
    /// Components of sprite element
    /// </summary>
    public interface ISpriteComponent : IImageComponent { }

    /// <summary>
    /// Components of raw texture element
    /// </summary>
    public interface ITextureComponent : IImageComponent { }

    /// <summary>
    /// Component of text element
    /// </summary>
    public interface ILabelComponent : IGraphicComponent { }

}
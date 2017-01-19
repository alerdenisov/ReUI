using System;
using Rentitas;
using UnityEngine;

namespace ReUI.Api
{
    public class ViewLink : IUIPool, IViewComponent
    {
        public Guid Id;
    }

    public class Name : IAttributeValue<string>, IViewComponent
    {
        public string Value { get; set; }
    }

    public class Size : IAttributeValue<Vector2>, IViewComponent
    {
        public Vector2 Value { get; set; }
    }

    public class Pivot : IAttributeValue<Vector2>, IViewComponent
    {
        public Vector2 Value { get; set; }
    }

    public class Margin : IAttributeValue<Vector4>, IViewComponent
    {
        public Vector4 Value { get; set; }
    }

    public class Position : IAttributeValue<Vector2>, IViewComponent
    {
        public Vector2 Value { get; set; }
    }

    public class Anchor : IAttributeValue<Vector4>, IViewComponent
    {
        public Vector4 Value { get; set; }
    }
    
    public class Rotation : IAttributeValue<float>, IViewComponent
    {
        public float Value { get; set; }
    }

    public class Scale : IAttributeValue<Vector3>, IViewComponent
    {
        public Vector3 Value { get; set; }
    }
}
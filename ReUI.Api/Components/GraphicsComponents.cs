using Rentitas;

namespace ReUI.Api
{
    public class GraphicType : IGraphicComponent, IFlag { }

    public class Graphic : IAttributeValue<UnityEngine.UI.Graphic>, IGraphicComponent
    {
        public UnityEngine.UI.Graphic Value { get; set; }
    }
}
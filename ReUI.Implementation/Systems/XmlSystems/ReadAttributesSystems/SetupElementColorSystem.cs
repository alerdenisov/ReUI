using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupElementColorSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            UnityEngine.Color color = UnityEngine.Color.white;
            Vector4 vecColor;

            if (xml.HasColor("Color", out color))
            {
                var colorComponent = entity.Need<Api.Color>();
                colorComponent.Value = color;
                entity.ReplaceInstance(colorComponent);
            }
        }
    }
}
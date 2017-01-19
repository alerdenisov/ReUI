using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupTextLineHeightSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (TextType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            float lineHeight;
            if (!xml.HasFloat("LineHeight", out lineHeight))
                lineHeight = 1.35f;

            var textAlignment = entity.Need<Api.LineSpacing>();
            textAlignment.Value = lineHeight;
            entity.ReplaceInstance(textAlignment);

        }
    }
}
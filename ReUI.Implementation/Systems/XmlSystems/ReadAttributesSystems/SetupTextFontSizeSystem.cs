using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupTextFontSizeSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (TextType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            int size;
            if(!xml.HasInt("FontSize", out size)) size = 15;

            var fontsize = entity.Need<FontSize>();
            fontsize.Value = size;
            entity.ReplaceInstance(fontsize);
        }
    }
}
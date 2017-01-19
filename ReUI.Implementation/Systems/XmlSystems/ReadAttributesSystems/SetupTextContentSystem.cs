using System;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupTextContentSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] { typeof(TextType) };

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            var text = entity.Need<Text>();
            text.Value = xml.Content.Trim();
            entity.AddInstance(text);
        }
    }
}
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
            var value = string.Empty;
            if (xml.Attributes.ContainsKey("Content"))
                value = xml.Attributes["Content"];
            else if (!string.IsNullOrEmpty(xml.Content))
                value = xml.Content;

            value = value.Trim();

            text.Value = value;
            entity.AddInstance(text);
        }
    }
}
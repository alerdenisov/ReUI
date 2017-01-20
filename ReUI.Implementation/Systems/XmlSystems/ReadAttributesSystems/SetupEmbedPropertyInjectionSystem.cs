using System;
using Rentitas;
using ReUI.Api;
using ReUI.Implementation.Helpers;

namespace ReUI.Implementation.Systems
{
    public class SetupEmbedPropertyInjectionSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (Embed)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            var value = "return {}";
            
            if (!string.IsNullOrEmpty(xml.Content) && xml.Content.Contains("return"))
                value = xml.Content;
            else if (xml.HasAttribute("Value"))
                value = xml.Attributes["Value"];
            else if (xml.HasAttribute("Props"))
                value = xml.Attributes["Props"];

            entity.SetAttribute<LuaCodePropertiesInjection, string>(value);
            entity.Add<LuaCode>(code => code.Value.Reset());
            entity.Toggle<LuaType>(true);
        }
    }
}
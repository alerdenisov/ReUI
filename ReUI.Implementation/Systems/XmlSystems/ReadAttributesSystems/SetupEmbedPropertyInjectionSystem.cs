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
            if (!string.IsNullOrEmpty(xml.Content) && xml.Content.Contains("return"))
                entity.SetAttribute<LuaCodePropertiesInjection, string>(xml.Content);
            else if(xml.HasAttribute("Value"))
                entity.SetAttribute<LuaCodePropertiesInjection, string>(xml.Attributes["Value"]);
            else
                entity.SetAttribute<LuaCodePropertiesInjection, string>("return {}");

            entity.Add<LuaCode>(code => code.Value.Reset());
            entity.Toggle<LuaType>(true);
        }
    }
}
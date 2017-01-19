using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupElementResourceSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            if (xml.HasAttribute("Resource"))
            {
                var res = entity.Need<Resource>();
                res.Value = xml.Attributes["Resource"];
                entity.ReplaceInstance(res);
            }
        }
    }
}
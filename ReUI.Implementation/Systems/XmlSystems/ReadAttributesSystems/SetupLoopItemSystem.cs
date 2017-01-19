using System;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupLoopItemSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (LoopType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            entity.Add<LoopItem>(i => i.Value = xml.Attributes["Component"]);
        }
    }
}
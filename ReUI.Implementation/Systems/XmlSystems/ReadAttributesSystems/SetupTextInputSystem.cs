using System;
using Rentitas;
using ReUI.Api;
using ReUI.Implementation;

namespace ReUI.Implementation.Systems
{
    public class SetupTextInputSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (TextInputType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {

        }
    }
}
using System.Collections.Generic;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupElementNameSystem : IReactiveSystem<IUIPool>
    {
        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var xml = entity.Get<XmlElement>();
                SetupFor(entity, xml);
            }
        }

        protected void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            var name = xml.HasAttribute("Name") ? xml.Attributes["Name"] : xml.Name;
            entity.Add<Name>(n => n.Value = name);
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof(Element), typeof(XmlElement)).OnEntityAdded();
    }
}
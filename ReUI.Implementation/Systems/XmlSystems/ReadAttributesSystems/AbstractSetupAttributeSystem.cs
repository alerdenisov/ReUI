using System;
using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public abstract class AbstractSetupAttributeSystem : IReactiveSystem<IUIPool>
    {
//        protected virtual Type[] NoneTypes => new Type[0];
        protected virtual Type[] EnsureTypes => new Type[0];

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                if(!entity.Has<XmlElement>() || !entity.Has<Element>())
                    continue;

                var xml = entity.Get<XmlElement>();
                SetupFor(entity, xml);
            }
        }

        protected abstract void SetupFor(Entity<IUIPool> entity, XmlElement xml);

        public TriggerOnEvent Trigger => Matcher
            .AllOf(new [] { typeof(Element), typeof(XmlElement) }.Concat(EnsureTypes).ToArray())
            .OnEntityAdded();
//            .NoneOf(NoneTypes).OnEntityAdded();
    }
}
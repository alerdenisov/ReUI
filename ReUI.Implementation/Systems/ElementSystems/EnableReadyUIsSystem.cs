using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class EnableReadyUIsSystem : IReactiveSystem<IUIPool>
    {
        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                entity.Toggle<Disabled>(false);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Element), typeof (Ready)).OnEntityAdded();
    }
}
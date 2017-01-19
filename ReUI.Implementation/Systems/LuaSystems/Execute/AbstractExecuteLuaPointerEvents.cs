using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine.EventSystems;

namespace ReUI.Implementation.LuaSystems
{
    public abstract class AbstractExecuteLuaPointerEvents<T> : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
        where T : IPointerComponent
    {
        protected Pool<IUIPool> Pool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var eventEntity in entities)
            {
                var eventComponent = eventEntity.Get<T>();
                var element = Pool.GetElement(eventComponent.Id);

                ExecuteEvent(element, eventComponent.Data);
            }
        }

        protected abstract void ExecuteEvent(Entity<IUIPool> element, PointerEventData data);

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (T)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            Pool = typedPool;
        }
    }
}
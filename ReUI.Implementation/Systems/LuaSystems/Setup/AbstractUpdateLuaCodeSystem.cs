using System;
using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public abstract class AbstractUpdateLuaCodeSystem<T> : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
        where T : ILuaComponent, IAttributeValue<string>
    {
        protected Pool<IUIPool> UIPool { get; private set; }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.IsEnabled) continue;

                if (entity.Has<T>())
                    OnUpdateCode(entity, entity.GetAttribute<T, string>());
                else
                    OnRemoveCode(entity);
            }
        }

        protected virtual void OnRemoveCode(Entity<IUIPool> entity) => OnUpdateCode(entity, null);
        protected abstract void OnUpdateCode(Entity<IUIPool> entity, string code);

        public TriggerOnEvent Trigger => Matcher
            .AllOf(typeof(Element), typeof(T))
            .OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            UIPool = typedPool;
        }
    }
}
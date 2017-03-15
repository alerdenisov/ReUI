using System;
using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public abstract class AbstractAttributeUpdateSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>, IExecuteSystem
    {
        protected abstract Type[] AttributeTypes { get; }
        protected virtual Type[] EnsureTypes => new Type[0];

        protected IViewProvider ViewPool;
        private HashSet<Entity<IUIPool>> _awaiting = new HashSet<Entity<IUIPool>>();

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                TrySetup(entity);

            }
        }

        private void TrySetup(Entity<IUIPool> entity)
        {
            if (!entity.Has<ViewLink>())
            {
                Debug.LogError("Unknown issue");
                return;
            }

            var link = entity.Get<ViewLink>();
            var view = ViewPool.GetByIdentity(link.Id);

            if (!view.InScene())
            {
                _awaiting.Add(entity);
            }
            else
            {
                SetupAttribute(entity, view);//, view.GetObject());
                _awaiting.Remove(entity);
            }
        }

        public void Execute()
        {
            foreach (var entity in _awaiting.ToArray())
                TrySetup(entity);
        }

        protected abstract void SetupAttribute(Entity<IUIPool> uiEntity, IView view);//, GameObject go);

        public TriggerOnEvent Trigger => Matcher
            .AllOf(new[] { typeof (Element), typeof(ViewLink) }.Concat(EnsureTypes).ToArray())
            .AnyOf(AttributeTypes)
            .OnEntityAdded();

        public virtual void SetPool(Pool<IUIPool> pool)
        {
            ViewPool = pool.Get<ViewProvider>().Value;
        }
    }
}
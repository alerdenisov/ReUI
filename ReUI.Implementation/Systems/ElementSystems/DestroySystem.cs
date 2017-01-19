using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class DestroySystem : ICleanupSystem, ISetPool<IUIPool>, IReactiveSystem<IUIPool>
    {
        private Group<IUIPool> _detroyingEntity;
        private Pool<IUIPool> _pool;
        private IViewProvider _viewPool;

        public void Cleanup()
        {
            foreach (var entity in _detroyingEntity.GetEntities())
            {
                if (entity.Has<ViewLink>())
                {
                    var id = entity.Get<ViewLink>().Id;
                    _viewPool.GetByIdentity(id)?.Destroy();
                }

                _pool.DestroyEntity(entity);
            }
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
            _viewPool = _pool.Get<ViewProvider>().Value;
            _detroyingEntity = typedPool.GetGroup(Matcher.AllOf(typeof (Destroy)));
        }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                RecursiveDestroy(entity);
//                var children = _pool.GetChildren(entity);
//                fore
//                if (!entity.Has<ViewLink>())
//                    continue;

//                var id = entity.Get<ViewLink>().Id;
//                _viewPool.GetByIdentity(id)?.Destroy();
            }
            
        }

        private void RecursiveDestroy(Entity<IUIPool> entity)
        {
            if (!entity.Has<Element>())
                return;

            var children = _pool.GetChildren(entity);
            foreach (var child in children)
            {
                child.Toggle<Destroy>(true);
                RecursiveDestroy(child);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Destroy)).OnEntityAdded();
    }
}
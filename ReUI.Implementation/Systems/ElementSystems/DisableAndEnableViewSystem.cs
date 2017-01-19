using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class DisableAndEnableViewSystem : ISetPool<IUIPool>, IReactiveSystem<IUIPool>
    {
        private IViewProvider _viewPool;

        public void SetPool(Pool<IUIPool> pool)
        {
            _viewPool = pool.Get<ViewProvider>().Value;
        }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                if(entity.Has<Destroy>())
                    continue;
                if (!entity.Has<ViewLink>())
                    continue;

                var view = _viewPool.GetByIdentity(entity.Get<ViewLink>().Id);
                view.SetActive(!entity.Has<Disabled>());
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof(Disabled)).OnEntityAddedOrRemoved();
    }
}
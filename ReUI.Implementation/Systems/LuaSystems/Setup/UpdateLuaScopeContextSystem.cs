using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems.LuaSystems
{
    public class UpdateLuaScopeContextSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _uiPool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var context = entity.Get<LuaScopeContext>().Value;
                var props = entity.Get<LuaScopeProps>().Value;
                context.Set("props", props);
                entity.Toggle<LuaScopePropsUpdate>(true);
            }
        }

        public TriggerOnEvent Trigger => Matcher
            .AllOf(typeof (ScopeType), typeof (LuaScopePropsUpdate))
            .OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
        }
    }
}
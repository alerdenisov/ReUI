using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems.LuaSystems
{
    public class InjectScopeStateToContextSystem : IMultiReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
    {
        private Pool<IUIPool> _uiPool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                Inject(entity);
            }
        }

        private void Inject(Entity<IUIPool> entity)
        {
            var context = entity.GetAttribute<LuaElementContext, ILuaTable>();
            var scope = _uiPool.GetScope(entity.Get<Scope>().Id);
            var scopeState = scope.GetAttribute<LuaScopeState, ILuaTable>();

            context.Set("state", scopeState);
            entity.SetAttribute<LuaElementContext, ILuaTable>(context);
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
        }

        public TriggerOnEvent[] Triggers => new[]
        {
            Matcher.AllOf(typeof (LuaType)).OnEntityAdded(),
            Matcher.AllOf(typeof (LuaScopeStateUpdate)).OnEntityAdded(),
        };

        public IMatcher EnsureComponents => Matcher.AllOf(typeof (Scope), typeof (LuaElementContext));
    }
}
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems.LuaSystems
{
    public class InjectScopePropsToContextSystem : IMultiReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
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
            var props = entity.GetAttribute<LuaScopeProps, ILuaTable>();

            context.Set("props", props);
            entity.SetAttribute<LuaElementContext, ILuaTable>(context);
            entity.Toggle<LuaScopePropsUpdate>(true);
            foreach (var element in _uiPool.GetElementsInScope(entity.Get<Scope>().Id))
            {
                element.Toggle<LuaScopeStateUpdate>(true);
            }
//            entity.Toggle<LuaScopeStateUpdate>(true);
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
        }

        public TriggerOnEvent[] Triggers => new[]
        {
            Matcher.AllOf(typeof (LuaType)).OnEntityAdded(),
            Matcher.AllOf(typeof (LuaScopeProps)).OnEntityAdded(),
        };

        public IMatcher EnsureComponents => Matcher.AllOf(typeof (ScopeType), typeof (LuaElementContext));
    }
}
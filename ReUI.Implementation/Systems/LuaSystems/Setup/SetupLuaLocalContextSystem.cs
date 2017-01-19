using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems.LuaSystems
{
    public class SetupLuaLocalContextSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private ILuaProvider _lua;
        private Pool<IUIPool> _uipool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var scope = _uipool.GetScope(entity.Get<Scope>().Id);
                var local = _lua.CreateContext();
                local.Set("self", new ElementTable(entity, _uipool));
                local.Set("scope", new ScopeTable(scope, _uipool));
                entity.SetAttribute<LuaElementContext, ILuaTable>(local);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (LuaType), typeof(Scope)).OnEntityAdded();
        public void SetPool(Pool<IUIPool> typedPool)
        {
            _lua = typedPool.Get<LuaProvider>().Value;
            _uipool = typedPool;
        }
    }
}
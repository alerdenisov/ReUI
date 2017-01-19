using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupScopeStateSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _pool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            var provider = _pool.Get<LuaProvider>().Value;
            foreach (var entity in entities)
                if(!entity.Has<LuaScopeState>())
                    entity.SetAttribute<LuaScopeState, ILuaTable>(provider.NewTable());
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (ScopeType)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
        }
    }
}
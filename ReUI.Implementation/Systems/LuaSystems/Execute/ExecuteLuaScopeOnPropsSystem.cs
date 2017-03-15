using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.LuaSystems
{
    public class ExecuteLuaScopeOnPropsSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
    {
        private Pool<IUIPool> _pool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                entity.Get<LuaLifeCycle>().OnProps();
                entity.Toggle<LuaScopePropsUpdate>(false);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof(LuaScopePropsUpdate)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
        }

        public IMatcher EnsureComponents => Matcher.AllOf(typeof (ScopeType), typeof (LuaCodeOnProps), typeof (LuaLifeCycle), typeof(LuaCompiled));
    }
}
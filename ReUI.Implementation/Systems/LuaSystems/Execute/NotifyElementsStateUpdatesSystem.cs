using System;
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.LuaSystems
{
    public class NotifyElementsStateUpdatesSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
    {
        private Pool<IUIPool> _uipool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var scopeEntity in entities)
            {
                var elements = _uipool.GetElementsInScope(scopeEntity.Get<Element>().Id);
                foreach (var element in elements)
                {
                    if (element.Has<LuaCodeOnState>() || 
                        element.Has<LuaCodePropertiesInjection>() ||
                        element.Has<LuaCodeLoopCollection>() ||
                        element.Has<LuaCodeLoopItteration>())
                    {
                        element.Toggle<LuaScopeStateUpdate>(true);
                    }
                }
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (LuaScopeState)).OnEntityAdded();

        public IMatcher EnsureComponents => Matcher.AllOf(typeof (ScopeType));

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uipool = typedPool;
        }
    }
}
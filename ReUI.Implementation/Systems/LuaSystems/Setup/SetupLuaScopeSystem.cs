using System;
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.LuaSystems
{
    public class SetupLuaScopeSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _uiPool;
        private ILuaProvider _lua;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var scopeEntity in entities)
            {
                ILuaTable props = null;
                ILuaTable state = _lua.NewTable();
                ILuaTable context = _lua.CreateContext();

                if (scopeEntity.Has<Parent>())
                {
                    // Child scope (embeded) - requesting props is required!
                    var parent = _uiPool.GetParent(scopeEntity);
                    props = parent.Has<LuaScopeProps>()
                        ? parent.GetAttribute<LuaScopeProps, ILuaTable>()
                        : _lua.NewTable();
                }
                else if (scopeEntity.Has<LuaScopeProps>())
                    props = scopeEntity.GetAttribute<LuaScopeProps, ILuaTable>();
                else
                // Root scope must create clean props table
                    props = _lua.NewTable();

                props.Set("Test", (Action)(() => Debug.Log("test")));

                context.Set("state", state);
                context.Set("props", props);
                context.Set("scope", new ScopeTable(scopeEntity, _uiPool));

                scopeEntity.SetAttribute<LuaScopeProps, ILuaTable>(props);
                scopeEntity.SetAttribute<LuaScopeState, ILuaTable>(state);
                scopeEntity.SetAttribute<LuaScopeContext, ILuaTable>(context);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (ScopeType)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
            _lua = typedPool.Get<LuaProvider>().Value;
        }
    }
}
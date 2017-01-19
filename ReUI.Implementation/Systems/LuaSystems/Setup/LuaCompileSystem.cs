using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Systems
{
    public class LuaCompileSystem : ISetPool<IUIPool>, IInitializeSystem, IReactiveSystem<IUIPool>
    {
        private Pool<IUIPool> _uiPool;
        private ILuaProvider _luaProvider;

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
        }
        public void Initialize()
        {
            _luaProvider = _uiPool.Get<LuaProvider>().Value;
        }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var context = entity.Get<LuaElementContext>().Value;
                var code = entity.Get<LuaCode>().Value;
                _luaProvider.Execute(code.ToString(), $"{entity.GetAttribute<Name, string>()}_{entity.Get<Element>().Id}", context);
                entity.SetAttribute<LuaCompiled, ILuaTable>(context);
                entity.Toggle<LuaRequireCompile>(false);
            }
        }

        public TriggerOnEvent Trigger
            => Matcher.AllOf(typeof (Element), typeof (LuaCode), typeof (LuaRequireCompile)).OnEntityAdded();
    }
}
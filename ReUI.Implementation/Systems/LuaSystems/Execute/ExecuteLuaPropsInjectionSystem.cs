using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Systems
{
    public class ExecuteLuaPropsInjectionSystem : ISetPool<IUIPool>, IReactiveSystem<IUIPool>, IEnsureComponents
    {
        private Group<IUIPool> _injectors;
        private Pool<IUIPool> _pool;
        private ILuaProvider _lua;

        private void InjectFrom(Entity<IUIPool> embed)
        {

            var cycle = embed.Get<LuaLifeCycle>();
            var props = cycle.PropertyInjection();
            var table = _lua.ToTable(props);

            embed.SetAttribute<LuaScopeProps, ILuaTable>(table);
            embed.Toggle<LuaScopeStateUpdate>(false);
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
            _lua = _pool.Get<LuaProvider>().Value;
        }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                InjectFrom(entity);
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (LuaScopeStateUpdate)).OnEntityAdded();
        
        public IMatcher EnsureComponents => Matcher.AllOf(
            typeof (Embed),
            typeof (LuaCodePropertiesInjection),
            typeof (LuaCompiled),
            typeof (LuaLifeCycle));
    }
}
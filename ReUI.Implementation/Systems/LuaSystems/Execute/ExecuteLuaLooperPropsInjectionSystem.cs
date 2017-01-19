using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.LuaSystems
{
    public class ExecuteLuaLooperPropsInjectionSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>,IEnsureComponents
    {
        private Pool<IUIPool> _uipool;
        private ILuaProvider _lua;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var id = entity.Get<Element>().Id;
                var children = _uipool.GetChildren(id).ToArray();
                var collection = entity.GetAttribute<LooperCollection, ILuaTable>();
                var keys = collection.GetKeys<int>().ToArray();
                var collectionCount = keys.Length;
                var injection = entity.Get<LuaLooperMethods>().GetItterationProperties;

                var index = 0;

                foreach (var child in children)
                {
                    if (index < collectionCount)
                    {
                        var props = injection(new ElementTable(child, _uipool), keys[index++]);
                        child.SetAttribute<LuaScopeProps, ILuaTable>(_lua.ToTable(props));
                    }
                    else
                    {
                        child.Toggle<Destroy>(true);
                    }
                }
            }
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uipool = typedPool;
            _lua = _uipool.Get<LuaProvider>().Value;
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (LooperCollection)).OnEntityAdded();
        public IMatcher EnsureComponents => Matcher.AllOf(typeof (LoopType), typeof (LuaLooperMethods));
    }
}
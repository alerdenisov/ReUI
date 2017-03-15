using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Systems
{
    public class ExeculeLuaLooperSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
    {
        private Pool<IUIPool> _uipool;
        private ILuaProvider _lua;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var id = entity.Get<Element>().Id;
                var methods = entity.Get<LuaLooperMethods>();
                var collection = _lua.ToTable(methods.GetCollection());
                var keys = collection.GetKeys<int>();
                var count = keys.Count();
                var item = entity.GetAttribute<LoopItem, string>();
                var children = _uipool.GetChildren(id);
                var childCount = children.Count;

                if (childCount < count)
                {
                    for (var i = 0; i < count - childCount; i++)
                    {
                        var child = _uipool.CreateChild(id);
                        child.Add<XmlElement>(xml =>
                        {
                            xml.Name = item;
                            xml.Attributes = new Dictionary<string, string>();
                            xml.Content = string.Empty;
                        })
                        .Add<Scope>(s => s.Id = child.Get<Element>().Id);
                    }
                }

                entity.Toggle<LuaScopeStateUpdate>(false);
                entity.SetAttribute<LooperCollection, ILuaTable>(collection);
            }
        }

        public TriggerOnEvent Trigger
            => Matcher.AllOf(typeof (LuaScopeStateUpdate)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uipool = typedPool;
            _lua = _uipool.Get<LuaProvider>().Value;
        }

        public IMatcher EnsureComponents
            => Matcher.AllOf(
                typeof (LoopItem), 
                typeof (LuaLooperMethods), 
                typeof (LuaCodeLoopItteration),
                typeof (LuaCodeLoopCollection));
    }
}
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
//                if (childCount > count)
//                {
//                    foreach (var source in children.Skip(count))
//                        source.Toggle<Disabled>(true);

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

//                foreach (var entity in children.Take(count))
//                {
//                    entity.Toggle<Disabled>(false);
//                }

                entity.Toggle<LuaScopeStateUpdate>(false);
                entity.SetAttribute<LooperCollection, ILuaTable>(collection);
//                var id = entity.Get<Element>().Id;
//                var methods = entity.Get<LuaLooperMethods>();
//                var collection = _lua.ToTable(methods.GetCollection());
//                var item = entity.GetAttribute<LoopItem, string>();
//                foreach (var key in collection.GetKeys<int>())
//                {
//                    var element = _uipool.CreateChild(id);//.Add<Embed>(e => e.Name = item);
//                    element.Add<XmlElement>(xml =>
//                    {
//                        xml.Name = item;
//                        xml.Attributes = new Dictionary<string, string>();
//                        xml.Content = string.Empty;
//                    });
//
//                    var table = new ElementTable(element, _uipool);
//                    var props = methods.GetItterationProperties(table, key);
//                    element.SetAttribute<LuaScopeProps, ILuaTable>(_lua.ToTable(props));
//                    element.Toggle<LuaScopePropsUpdate>(true);
//                    element.Toggle<LuaRequireCompile>(true);
//                }
//
//                entity.Toggle<LuaScopePropsUpdate>(false);
            }
        }

        public TriggerOnEvent Trigger
            =>
                Matcher.AllOf(typeof (LuaScopeStateUpdate)).OnEntityAdded();

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
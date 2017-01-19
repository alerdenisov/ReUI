using System;
using Rentitas;

namespace ReUI.Api
{
    public static class UICreationExtensions 
    {
        public static Entity<IUIPool> CreateRoot(this Pool<IUIPool> @this)
        {
            var root = @this.CreateScope(0);
//            root.SetAttribute<LuaScopeProps, ILuaTable>(@this.Get<LuaProvider>().Value.NewTable());
            return root.Toggle<Root>(true);
        }

        public static Entity<IUIPool> CreateChild(this Pool<IUIPool> @this, Guid id, int order = 0)
        {
            var childEntity = @this.CreateElement(order);
            var parent = childEntity.CreateComponent<Parent>();
            parent.Id = id;

            return childEntity.AddInstance(parent);
        }

        private static Entity<IUIPool> CreateScope(this Pool<IUIPool> @this, int order)
        {
            var scope = @this.CreateElement(order);
            return scope;
        }

        private static Entity<IUIPool> CreateElement(this Pool<IUIPool> @this, int order)
        {
            var element = @this.CreateEntity();
            return element.AddElement(order);
        }

        public static Entity<IUIPool> AddElement(this Entity<IUIPool> @this, int order = 0)
        {
            if (@this.Has<Element>())
                return @this;

            var el = @this.CreateComponent<Element>();
            el.Id = Guid.NewGuid();

            @this.Toggle<Api.Disabled>(true);

            var ord = @this.Need<Order>();
            ord.Value = order;

            return @this.AddInstance(el).AddInstance(ord);
        }
    }
}
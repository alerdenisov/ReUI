using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public static class ElementExtensions
    {
        public static ILuaTable ToTable(this Entity<IUIPool> entity, ILuaEnvironment env)
        {
            var tbl = env.NewTable();
            tbl.Set("__index", entity);
            tbl.Set("id", entity.Get<Element>().Id.ToString());
            tbl.Set("name", entity.Get<Api.Name>().Value);
//            tbl.Set("parent", (Func<LuaTable>)(() => ToTable(_uiPool.GetElement(element.Get<Api.Parent>().Id))));
            return tbl;
        }

        public static Entity<IUIPool> SetAttribute<TAttribute, TValue>(this Entity<IUIPool> entity, TValue value)
            where TAttribute : IAttributeValue<TValue>, IUIPool, new()
        {
            var attr = entity.Need<TAttribute>();
            attr.Value = value;
            entity.ReplaceInstance(attr);
            return entity;
        }
    }
}
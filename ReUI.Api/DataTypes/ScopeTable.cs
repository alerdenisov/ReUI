using Rentitas;

namespace ReUI.Api
{
    [ExposeToLua]
    public class ScopeTable : ElementTable  
    {
        public ScopeTable(Entity<IUIPool> element, Pool<IUIPool> uiPool) : base(element, uiPool)
        {
        }

        public void setState(string key, object value)
        {
            var state = Element.Get<LuaScopeState>().Value;
            state.SetInPath(key, value);
            Element.SetAttribute<LuaScopeState, ILuaTable>(state);
            Element.Toggle<LuaScopeStateUpdate>(true);
        }

        public void setProps(string key, object value)
        {
//            var props = Element.Get<LuaScopeProps>().Value;
//            props.SetInPath(key, value);
//            Element.SetAttribute<LuaScopeProps, ILuaTable>(props);
        }
    }
}
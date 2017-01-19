using Rentitas;
using UnityEngine.EventSystems;

namespace ReUI.Api
{
    [ExposeToLua] public delegate void OnPointerFunc(PointerEventData data);
    [ExposeToLua] public delegate void ParamlessFunc();
    [ExposeToLua] public delegate object GetPropertiesFunc();
    [ExposeToLua] public delegate object GetCollectionFunc();
    [ExposeToLua] public delegate object GetItterationPropertiesFunc(ElementTable item, int index);

    public class LuaCompiledPointer : ILuaCompiledComponent
    {
        public OnPointerFunc OnMouseOver;
        public OnPointerFunc OnMouseOut;
        public OnPointerFunc OnClick;

        public OnPointerFunc OnDragStart;
        public OnPointerFunc OnDragEnd;

        public OnPointerFunc OnMouseWheel;
    }

    public class LuaLooperMethods : ILuaCompiledComponent
    {
        public GetCollectionFunc GetCollection;
        public GetItterationPropertiesFunc GetItterationProperties;
    }

    public class LuaLifeCycle : ILuaCompiledComponent
    {
        public ParamlessFunc OnTick;
        public ParamlessFunc OnInit;
        public ParamlessFunc OnCreate;
        public ParamlessFunc OnDisable;
        public ParamlessFunc OnDestroy;
        public ParamlessFunc OnProps;
        public ParamlessFunc OnState;
        public GetPropertiesFunc PropertyInjection;
    }

    public class LuaPropertiesExecute : ILuaCompiledComponent
    {
    }

    public class LuaLoop : ILuaCompiledComponent, ILoopComponents
    {
        public GetCollectionFunc GetCollection;
        public GetItterationPropertiesFunc GetProperties;
    }
    public class LooperCollection : TableComponent, ILoopComponents { }

    public class LuaRequireCompile : ILuaComponent, IFlag { }
    public class LuaScopePropsUpdate : ILuaComponent, IFlag { }
    public class LuaScopeStateUpdate : ILuaComponent, IFlag { }

    public abstract class TableComponent : IAttributeValue<ILuaTable>, ILuaCompiledComponent, IScopeComponent
    {
        private ILuaTable _value;

        public ILuaTable Value
        {
            get { return _value; }
            set
            {
                if (_value != null && _value.Equals(value))
                    return;

                _value?.Dispose();
                _value = value;
            }
        }
    }

    public class LuaScopeState : TableComponent { }

    public class LuaScopeProps : TableComponent { }

    public class LuaScopeContext : TableComponent { }

    public class LuaElementContext : TableComponent { }

    public class LuaCompiled : TableComponent { }


    public class LuaCode : ILuaComponent
    {
        public LuaCodeBuilder Value { get; set; }

        public LuaCode()
        {
            Value = new LuaCodeBuilder();
        }
    }

    public class LuaCodePropertiesInjection : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnTick : IAttributeValue<string>, ILuaCodeComponent {
        public string Value { get; set; }
    }

    public class LuaCodeOnDestroy : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }
    public class LuaCodeOnCreate : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnProps : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnState : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnClick : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnInit : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnMouseOver : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnMouseOut : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnRelease : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }

    public class LuaCodeOnPress : IAttributeValue<string>, ILuaCodeComponent
    {
        public string Value { get; set; }
    }
}
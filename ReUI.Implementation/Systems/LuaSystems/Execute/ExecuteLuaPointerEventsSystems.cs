using Rentitas;
using ReUI.Api;
using UnityEngine.EventSystems;

namespace ReUI.Implementation.LuaSystems
{
    public class ExecuteLuaOnMouseOverSystem : AbstractExecuteLuaPointerEvents<PointerOver>
    {
        protected override void ExecuteEvent(Entity<IUIPool> element, PointerEventData data)
        {
            if (!element.Has<LuaCompiledPointer>())
                return;

            var pointer = element.Get<LuaCompiledPointer>();
            pointer.OnMouseOver?.Invoke(data);
        }
    }
    public class ExecuteLuaOnMouseOutSystem : AbstractExecuteLuaPointerEvents<PointerOut>
    {
        protected override void ExecuteEvent(Entity<IUIPool> element, PointerEventData data)
        {
            if (!element.Has<LuaCompiledPointer>())
                return;

            var pointer = element.Get<LuaCompiledPointer>();
            pointer.OnMouseOut?.Invoke(data);
        }
    }
    public class ExecuteLuaOnClickSystem : AbstractExecuteLuaPointerEvents<PointerClick>
    {
        protected override void ExecuteEvent(Entity<IUIPool> element, PointerEventData data)
        {
            if (!element.Has<LuaCompiledPointer>())
                return;

            var pointer = element.Get<LuaCompiledPointer>();
            pointer.OnClick?.Invoke(data);
        }
    }
}
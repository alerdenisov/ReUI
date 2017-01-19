using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaPointerSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[]
        {
            typeof (LuaCodeOnClick),
            typeof (LuaCodeOnMouseOut),
            typeof (LuaCodeOnMouseOver)
        };

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
//            uiEntity.Need<LuaCodeBuilder>();
//            var luaExecution = go.RequireComponent<UILuaPointer>();
//            luaExecution.OnClickCode = CheckLua<LuaOnClick>(uiEntity);
//            luaExecution.OnMouseOutCode = CheckLua<LuaOnMouseOut>(uiEntity);
//            luaExecution.OnMouseOverCode = CheckLua<LuaOnMouseOver>(uiEntity);
        }

        private string CheckLua<T>(Entity<IUIPool> uiEntity) where T : ILuaComponent, IAttributeValue<string>
        {
            if (uiEntity.Has<T>())
                return uiEntity.Get<T>().Value;

            return string.Empty;
        }
    }
}
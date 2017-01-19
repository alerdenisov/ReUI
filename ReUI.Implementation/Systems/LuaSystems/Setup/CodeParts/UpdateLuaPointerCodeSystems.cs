using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaOnMouseOverCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnMouseOver>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.MouseOver, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }

    public class UpdateLuaOnMouseOutCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnMouseOut>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.MouseOut, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }

    public class UpdateLuaOnClickCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnClick>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.Click, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
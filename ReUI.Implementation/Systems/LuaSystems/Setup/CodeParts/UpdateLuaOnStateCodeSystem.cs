using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaOnStateCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnState>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.State, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
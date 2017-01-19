using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaOnPropsCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnProps>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.Props, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
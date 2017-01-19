using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaOnTickCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeOnTick>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.Tick, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateLuaLooperItterationCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeLoopItteration>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.LoopItteration, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }

    public class UpdateLuaLooperCollectionCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodeLoopCollection>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.LoopCollection, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
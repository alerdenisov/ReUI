using System;
using Rentitas;
using ReUI.Api;
using UnityEngine; 

namespace ReUI.Implementation
{
    public class UpdateLuaPropertyInjectionCodeSystem : AbstractUpdateLuaCodeSystem<LuaCodePropertiesInjection>
    {
        protected override void OnUpdateCode(Entity<IUIPool> entity, string code)
        {
            entity.SetupCode(ExecutionMethod.PropertyInjection, code);
            entity.Toggle<LuaRequireCompile>(true);
        }
    }
}
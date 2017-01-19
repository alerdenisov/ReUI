using System;
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems
{
    public class SetupLuaLooperSystem : IReactiveSystem<IUIPool>
    {
        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var looper = entity.Need<LuaLooperMethods>();

                var compiled = entity.GetAttribute<LuaCompiled, ILuaTable>();
                compiled.Get(ExecutionMethod.LoopCollection.ToString(), out looper.GetCollection);
                compiled.Get(ExecutionMethod.LoopItteration.ToString(), out looper.GetItterationProperties);

                if(looper.GetCollection == null)
                    throw new NullReferenceException("Looper is require Collection method");

                if(looper.GetItterationProperties == null)
                    throw new NullReferenceException("Looper is require Itteration method");

                entity.ReplaceInstance(looper);
            }
        }

        public TriggerOnEvent Trigger
            =>
                Matcher.AllOf(
                    typeof(LoopType), 
                    typeof(LuaCompiled), 
                    typeof(LuaCodeLoopItteration),
                    typeof(LuaCodeLoopCollection)).OnEntityAdded();
    }
}
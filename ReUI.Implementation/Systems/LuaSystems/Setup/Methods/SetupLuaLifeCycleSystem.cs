using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems
{
    public class SetupLuaLifeCycleSystem : IReactiveSystem<IUIPool>
    {
        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.IsEnabled)
                    return;
                if (!entity.Has<LuaCompiled>())
                    entity.Remove<LuaLifeCycle>();
                else
                {
                    MakeFor(entity);
                }
            }
        }

        private void MakeFor(Entity<IUIPool> entity)
        {
            var cycle = entity.Need<LuaLifeCycle>();
            var compiled = entity.GetAttribute<LuaCompiled, ILuaTable>();

            compiled.Get(ExecutionMethod.Tick.ToString(),       out cycle.OnTick);
            compiled.Get(ExecutionMethod.Create.ToString(),     out cycle.OnCreate);
            compiled.Get(ExecutionMethod.Destroy.ToString(),    out cycle.OnDestroy);
            compiled.Get(ExecutionMethod.Props.ToString(),      out cycle.OnProps);
            compiled.Get(ExecutionMethod.State.ToString(),      out cycle.OnState);
            compiled.Get(ExecutionMethod.PropertyInjection.ToString(), out cycle.PropertyInjection);

            entity.ReplaceInstance(cycle);
        }

        public TriggerOnEvent Trigger
            =>
                Matcher
                    .AllOf(typeof (LuaCompiled))
                    .AnyOf(
                        typeof (LuaCodeOnTick), 
                        typeof (LuaCodeOnInit), 
                        typeof (LuaCodeOnCreate), 
                        typeof (LuaCodeOnDestroy), 
                        typeof (LuaCodeOnProps),
                        typeof (LuaCodeOnState),
                        typeof (LuaCodePropertiesInjection))
                    .OnEntityAdded();

    }
}
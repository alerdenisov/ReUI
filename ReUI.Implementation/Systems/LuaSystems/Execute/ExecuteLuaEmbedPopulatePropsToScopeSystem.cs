using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;

namespace Assets.ReUI.Implementation.Systems.LuaSystems.Execute
{
    public class ExecuteLuaEmbedPopulatePropsToScopeSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>, IEnsureComponents
    {
        private Pool<IUIPool> _pool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var embed in entities)
            {
                var table = embed.GetAttribute<LuaScopeProps, ILuaTable>();
                var child = _pool.GetChildren(embed).Where(c => c.Has<ScopeType>());

                foreach (var entity in child)
                {
                    entity.SetAttribute<LuaScopeProps, ILuaTable>(table);
                }
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (LuaScopeProps)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
        }

        public IMatcher EnsureComponents => Matcher.AllOf(typeof(Embed));
    }
}
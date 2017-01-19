using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Systems
{
    public class ExecuteLuaTickSystem : ISetPool<IUIPool>, IExecuteSystem, IInitializeSystem
    {
        private Pool<IUIPool> _uiPool;
        private Group<IUIPool> _tickers;

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
        }

        public void Execute()
        {
            foreach (var entity in _tickers.GetEntities())
            {
//                Debug.Log($"Execute tick on {entity.GetAttribute<Name, string>()}");
                var cycle = entity.Get<LuaLifeCycle>();
                cycle.OnTick();
            }
        }

        public void Initialize()
        {
            _tickers = _uiPool.GetGroup(Matcher.AllOf(typeof (Element), typeof(LuaCompiled), typeof (LuaCodeOnTick), typeof (LuaLifeCycle)));
        }
    }
}
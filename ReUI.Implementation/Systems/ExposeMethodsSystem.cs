using System;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class ExposeMethodsSystem : IInitializeSystem, ISetPool<IUIPool>
    {
        private ILuaProvider _luaPool;
        private Pool<IUIPool> _uiPool;

        public void Initialize()
        {
//            var uiTable = _luaPool.CreateTable();
//            uiTable.Set("vars", _luaPool.CreateTable());
//            _luaPool.Expose("ui", uiTable);
//            _luaPool.ExposeMethod("ui.vars.get", (key) => _uiPool.GetOrCreateVariable(key.ToString()));
//            _luaPool.ExposeMethod("ui.vars.listen", (listener, key) => _uiPool.ListenAndGetVariable(key.ToString(), (IVariableListener)listener));
        }

        public void SetPool(Pool<IUIPool> pool)
        {
            _uiPool = pool;
            _luaPool = _uiPool.Get<LuaProvider>().Value;
        }
    }
}
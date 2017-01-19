using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class InitializeDependencySingletons : ISetPool<IUIPool>
    {
        private Pool<IUIPool> _pool;
        private readonly IContentProvider _contentProvider;
        private readonly IViewProvider _viewProvider;
        private readonly ILuaProvider _luaProvider;

        public InitializeDependencySingletons(IContentProvider contentProvider, IViewProvider viewProvider, ILuaProvider luaProvider)
        {
            _contentProvider = contentProvider;
            _viewProvider = viewProvider;
            _luaProvider = luaProvider;
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
            _pool.CreateEntity()
                .Add<ContentProvider>(p => p.Value = _contentProvider)
                .Add<LuaProvider>(p => p.Value = _luaProvider)
                .Add<ViewProvider>(p => p.Value = _viewProvider);
        }
    }
}
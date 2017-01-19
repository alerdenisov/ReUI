using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class UIElementIsReadySystem : IExecuteSystem, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _uiPool;
        private IViewProvider _viewPool;
        private Group<IUIPool> _awaitingUis;

        public void Execute()
        {
            foreach (var entity in _awaitingUis.GetEntities())
            {
                if (IsReady(entity))
                    entity.Toggle<Ready>(true);
            }
        }

        private bool IsReady(Entity<IUIPool> entity)
        {
            var view = _viewPool.GetByIdentity(entity.Get<ViewLink>().Id);
            if (!view.InScene())
                return false;

            var go = view.GetObject();

            if (go == null)
                return false;

            if (!go.transform.parent)
                return false;

            return true;
        }

        public void SetPool(Pool<IUIPool> pool)
        {
            _viewPool = pool.Get<ViewProvider>().Value;
            _uiPool = pool;//.Get<IUIPool>();

            _awaitingUis = _uiPool.GetGroup(Matcher.AllOf(typeof (Element), typeof (ViewLink)).NoneOf(typeof (Ready)));
        }
    }
}
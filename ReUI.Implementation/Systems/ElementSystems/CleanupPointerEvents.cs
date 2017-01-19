using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class CleanupPointerEvents : ISetPool<IUIPool>, ICleanupSystem
    {
        private Pool<IUIPool> _pool;
        private Group<IUIPool> _events;

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
            _events = _pool.GetGroup(Matcher.AnyOf(
                typeof (PointerClick),
                typeof (PointerOut),
                typeof (PointerOver),
                typeof (PointerPress),
                typeof (PointerRelease)));
        }

        public void Cleanup()
        {
            foreach (var entity in _events.GetEntities())
            {
                _pool.DestroyEntity(entity);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementOrderSystem : ISetPool<IUIPool>, IReactiveSystem<IUIPool>, IEnsureComponents, IExecuteSystem
//AbstractAttributeUpdateSystem
    {
        private Group<IUIPool> _orderings;
        private IViewProvider _viewPool;

        protected void SetupAttribute(Entity<IUIPool> uiEntity, GameObject go)
        {
            // todo!
//            go.transform.SetAsFirstSibling();//SetSiblingIndex(uiEntity._creationIndex);//.Get<Order>().Value * 10);
        }

        public void Execute()
        {
//            Execute(_orderings.GetEntities().ToList());
        }

        public void SetPool(Pool<IUIPool> pool)
        {
            _viewPool = pool.Get<ViewProvider>().Value;
            _orderings = pool.GetGroup(Matcher.AllOf(typeof (Element), typeof (Order), typeof (ViewLink), typeof(Ready)));
        }

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var viewId = entity.Get<ViewLink>().Id;
                var view = _viewPool.GetByIdentity(viewId);
                if (view == null || !view.InScene())
                    continue;

                view.SetOrder(entity.GetAttribute<Order, int>());//.GetObject()?.transform.SetSiblingIndex(entity.Get<Order>().Value);
//                view.GetObject().transform.SetAsLastSibling();
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Ready)).OnEntityAdded();
        public IMatcher EnsureComponents => Matcher.AllOf(typeof (Element), typeof (Order), typeof (ViewLink));
    }
}
using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementParentSystem : AbstractAttributeUpdateSystem
    {
        protected Pool<IUIPool> UIPool;

        protected override Type[] EnsureTypes => new[] {typeof (ChildrenType), typeof (Ready)};
        protected override Type[] AttributeTypes => new[] {typeof (Parent)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            Debug.Log($"Update parent for {uiEntity.GetAttribute<Name, string>()}");
            var parentId = uiEntity.Get<Parent>().Id;
            var parent = UIPool.GetElement(parentId);
            var parentView = ViewPool.GetByIdentity(parent.Get<ViewLink>().Id);
            view.SetParent(parentView);
        }

        public override void SetPool(Pool<IUIPool> pool)
        {
            UIPool = pool;
            base.SetPool(pool);
        }
    }
}
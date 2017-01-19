using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateTextResourceSystem : AbstractAttributeUpdateSystem
    {
        private IContentProvider _contentPool;

        protected override Type[] AttributeTypes => new[] {typeof (Resource)};
        protected override Type[] EnsureTypes => new[] {typeof (TextType) };

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var path = uiEntity.GetAttribute<Resource, string>();
            _contentPool.Request<UnityEngine.Font>(path, delegate (string p, UnityEngine.Font content)
            {
                go.GetComponent<UnityEngine.UI.Text>().font = content;
            });
        }

        public override void SetPool(Pool<IUIPool> pool)
        {
            _contentPool = pool.Get<ContentProvider>().Value;
            base.SetPool(pool);
        }
    }
}
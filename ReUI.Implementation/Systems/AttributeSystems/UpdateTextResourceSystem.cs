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
            _contentPool.RequestFont(path, delegate (string p, ContentReceiveResult<UnityEngine.Font> result)
            {
                if (!result.IsError)
                    go.GetComponent<UnityEngine.UI.Text>().font = result.Data;
                else
                    Debug.LogError(result.ErrorMessage);
            });
        }

        public override void SetPool(Pool<IUIPool> pool)
        {
            _contentPool = pool.Get<ContentProvider>().Value;
            base.SetPool(pool);
        }
    }
}
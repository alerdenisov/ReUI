using System;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;
using UnityEngine.UI;
using Graphic = ReUI.Api.Graphic;
using Sprite = UnityEngine.Sprite;

namespace ReUI.Implementation
{
    public class UpdateSpriteResourceSystem : AbstractAttributeUpdateSystem
    {
        private IContentProvider _contentPool;

        protected override Type[] AttributeTypes => new[] {typeof (Resource)};
        protected override Type[] EnsureTypes => new[] {typeof (SpriteType)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var path = uiEntity.GetAttribute<Resource, string>();
            _contentPool.RequestSprite(path, delegate(string p, ContentReceiveResult<Sprite> result)
            {
                if (!result.IsError)
                    go.GetComponent<Image>().sprite = result.Data;
                else
                    Debug.LogError(result.ErrorMessage);
            });
        }

        public override void SetPool(Pool<IUIPool> pools)
        {
            _contentPool = pools.Get<ContentProvider>().Value;//.Get<IContentPool>();
            base.SetPool(pools);
        }
    }
}
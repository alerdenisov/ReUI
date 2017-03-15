using System;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;
using UnityEngine.UI;
using Graphic = ReUI.Api.Graphic;
using Sprite = UnityEngine.Sprite;
using Texture = UnityEngine.Texture;

namespace ReUI.Implementation
{
    public class UpdateTextureResourceSystem : AbstractAttributeUpdateSystem
    {
        private IContentProvider _contentPool;

        protected override Type[] AttributeTypes => new[] {typeof (Resource)};
        protected override Type[] EnsureTypes => new[] {typeof (TextureType)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            var path = uiEntity.GetAttribute<Resource, string>();
            _contentPool.RequestTexture(path, delegate(string p,ContentReceiveResult<Texture> result)
            {
                if (!result.IsError)
                    view.SetTexture(result.Data);//go.GetComponent<RawImage>().texture = result.Data;
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
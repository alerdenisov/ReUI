using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;
using UnityEngine.UI;
using Graphic = UnityEngine.UI.Graphic;
using Object = UnityEngine.Object;
using Text = UnityEngine.UI.Text;

namespace ReUI.Implementation
{
    public class UpdateElementTypeSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[]
        {
            typeof(ElementType)
        };

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            var type = uiEntity.Get<ElementType>().Value;
            switch (type)
            {
                case Elements.Sprite:
                    UpdateAsSprite(uiEntity, view);//, go);
                    break;
                case Elements.RawImage:
                    UpdateAsTexture(uiEntity, view);//, go);
                    break;
                case Elements.Text:
                    UpdateAsLabel(uiEntity, view);//, go);
                    break;
                    case Elements.Root:
                    UpdateAsContext(uiEntity, view);//, go);
                    break;
            }
        }

        private void UpdateAsContext(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            view.RemoveComponent<Graphic>();
        }

        private void UpdateAsLabel(Entity<IUIPool> entity, IView view)//, GameObject go)
        {
            var text = UpdateAs<Text>(entity, view);
            text.font = (UnityEngine.Font)Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf");
        }

        private void UpdateAsTexture(Entity<IUIPool> entity, IView view)//, GameObject go)
        {
            UpdateAs<RawImage>(entity, view);
        }

        private void UpdateAsSprite(Entity<IUIPool> entity, IView view)//, GameObject go)
        {
            UpdateAs<Image>(entity, view);
        }

        private T UpdateAs<T>(Entity<IUIPool> entity, IView view)//, GameObject go) 
            where T : UnityEngine.UI.Graphic
        {
            var gfx = view.RequireComponent<T>();
            gfx.raycastTarget = false;
            entity.Replace<Api.Graphic>(g => g.Value = gfx);

            return gfx;
        }
    }
}
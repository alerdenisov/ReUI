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

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var type = uiEntity.Get<ElementType>().Value;
            switch (type)
            {
                case Elements.Sprite:
                    UpdateAsSprite(uiEntity, go);
                    break;
                case Elements.RawImage:
                    UpdateAsTexture(uiEntity, go);
                    break;
                case Elements.Text:
                    UpdateAsLabel(uiEntity, go);
                    break;
                    case Elements.Root:
                    UpdateAsContext(uiEntity, go);
                    break;
            }
        }

        private void UpdateAsContext(Entity<IUIPool> uiEntity, GameObject go)
        {
            var gfx = go.GetComponent<Graphic>();
            if (gfx) Object.Destroy(gfx);

//            go.RequireComponent<UILuaContext>();
        }

        private void UpdateAsLabel(Entity<IUIPool> entity, GameObject go)
        {
            UpdateAs<Text>(entity, go);
            (entity.Get<Api.Graphic>().Value as Text).font = (UnityEngine.Font)Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf");
        }

        private void UpdateAsTexture(Entity<IUIPool> entity, GameObject go)
        {
            UpdateAs<RawImage>(entity, go);
        }

        private void UpdateAsSprite(Entity<IUIPool> entity, GameObject go)
        {
            UpdateAs<Image>(entity, go);
        }

        private void UpdateAs<T>(Entity<IUIPool> entity, GameObject go) where T : UnityEngine.UI.Graphic
        {
            var current = go.GetComponent<Graphic>();
            if (current is T) return;

            Object.Destroy(current);
            var comp = go.AddComponent<T>();
            comp.raycastTarget = false;
            entity.Replace<Api.Graphic>(g => g.Value = comp);
        }
    }
}
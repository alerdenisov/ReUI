using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementRectSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[]
        {
            typeof (Margin),
            typeof (Anchor),
            typeof (Pivot),
            typeof (Size)
        };

        protected override void SetupAttribute(Entity<IUIPool> element, IView view, GameObject go)
        {
            var mrg = element.Get<Margin>().Value;
            var anc = element.Get<Anchor>().Value;
            var pvt = element.Get<Pivot>().Value;
            var siz = element.Get<Size>().Value;

            var rect = go.transform as RectTransform;

            rect.anchorMin = new Vector2(anc.x, anc.y);
            rect.anchorMax = new Vector2(anc.z, anc.w);

            rect.offsetMin = new Vector2(-mrg.w, -mrg.z);
            rect.offsetMax = new Vector2(mrg.y, mrg.x);

            rect.pivot = pvt;
            rect.sizeDelta = new Vector2(siz.x - mrg.x - mrg.z, siz.y - mrg.y - mrg.w);

        }
    }
}
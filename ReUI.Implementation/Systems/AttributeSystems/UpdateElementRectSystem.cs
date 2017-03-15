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

        protected override void SetupAttribute(Entity<IUIPool> element, IView view)//, GameObject go)
        {
            var mrg = element.Get<Margin>().Value;
            var anc = element.Get<Anchor>().Value;
            var pvt = element.Get<Pivot>().Value;
            var siz = element.Get<Size>().Value;

            view.SetAnchor(anc);
            view.SetOffset(new Vector4(-mrg.w, -mrg.z, mrg.y, mrg.x));
            view.SetPivot(pvt);
            view.SetSize(new Vector2(siz.x - mrg.x - mrg.z, siz.y - mrg.y - mrg.w));

        }
    }
}
using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateTextLineHeightSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (Api.TextAlignment) };
        protected override Type[] EnsureTypes    => new[] {typeof (TextType)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var text = uiEntity.Get<Graphic>().Value as UnityEngine.UI.Text;
            text.alignment = uiEntity.Get<Api.TextAlignment>().Value;
        }
    }
}
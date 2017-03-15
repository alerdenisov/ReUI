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

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            view.SetTextAlignment(uiEntity.GetAttribute<Api.TextAlignment, TextAnchor>());
        }
    }
}
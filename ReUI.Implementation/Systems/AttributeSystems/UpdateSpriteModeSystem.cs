using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;
using UnityEngine.UI;

namespace ReUI.Implementation
{
    public class UpdateSpriteModeSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (SplitMode)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var sprite = uiEntity.Get<Api.Graphic>().Value as Image;
            sprite.type = uiEntity.Get<SplitMode>().Value;
        }
    }
}
using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateTextAlignmentSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (LineSpacing) };
        protected override Type[] EnsureTypes    => new[] {typeof (TextType)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            var text = uiEntity.Get<Graphic>().Value as UnityEngine.UI.Text;
            text.lineSpacing = uiEntity.Get<LineSpacing>().Value;
        }
    }
}
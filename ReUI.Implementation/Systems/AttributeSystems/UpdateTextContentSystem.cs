using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateTextContentSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] { typeof(Text) };

        protected override Type[] EnsureTypes => new[] {typeof (TextType)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            go.GetComponent<UnityEngine.UI.Text>().text = uiEntity.Get<Text>().Value;
        }
    }
}
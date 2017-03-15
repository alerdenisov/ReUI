using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentitas;
using ReUI.Api;
using UnityEngine;
using UnityEngine.UI;

namespace ReUI.Implementation.Systems
{
    public class UpdateTextInputSystem : AbstractAttributeUpdateSystem
    {
        private Pool<IUIPool> _uipool;

        protected override Type[] AttributeTypes => new[] {typeof (TextInputType)};
        protected override Type[] EnsureTypes => new [] { typeof(Ready) };

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            var children = _uipool.GetChildren(uiEntity);

            var field = view.RequireComponent<InputField>();
            var text = children.FirstOrDefault(e => e.Has<TextType>());
            var sprite = children.FirstOrDefault(e => e.Has<SpriteType>());

            var textComponent = text?.Get<Api.Graphic>().Value as UnityEngine.UI.Text;
            var imageComponent = sprite?.Get<Api.Graphic>().Value;

            if (textComponent && imageComponent)
            {
                textComponent.supportRichText = false;
                imageComponent.raycastTarget = true;

                field.textComponent = textComponent;
                field.targetGraphic = imageComponent;
            }
        }

        public override void SetPool(Pool<IUIPool> pool)
        {
            _uipool = pool;
            base.SetPool(pool);
        }
    }
}

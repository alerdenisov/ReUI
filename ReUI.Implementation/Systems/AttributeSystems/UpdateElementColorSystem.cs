using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementColorSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (Api.Color)};

        protected override Type[] EnsureTypes => new[] {typeof ( Api.Graphic )};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            uiEntity.Get<Api.Graphic>().Value.color = uiEntity.Get<Api.Color>().Value;
        }
    }
}
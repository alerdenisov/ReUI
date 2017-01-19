using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementNameSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (Api.Name)};

        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view, GameObject go)
        {
            view.SetName(uiEntity.Get<Name>().Value);
        }
    }
}
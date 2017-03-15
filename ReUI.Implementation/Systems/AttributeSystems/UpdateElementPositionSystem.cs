using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class UpdateElementPositionSystem : AbstractAttributeUpdateSystem
    {
        protected override Type[] AttributeTypes => new[] {typeof (Position)};
        protected override void SetupAttribute(Entity<IUIPool> uiEntity, IView view)//, GameObject go)
        {
            var pos = uiEntity.GetAttribute<Position, Vector2>();
            view.SetPosition(pos);
        }
    }
}
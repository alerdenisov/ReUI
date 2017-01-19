using System;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Helpers
{
    public class SetupElementPositionSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            Vector2 pos;
            if (!xml.HasVector2("Position", out pos)) return;

            var position = entity.Need<Position>();
            position.Value = pos;
            entity.ReplaceInstance(position);
        }
    }
}
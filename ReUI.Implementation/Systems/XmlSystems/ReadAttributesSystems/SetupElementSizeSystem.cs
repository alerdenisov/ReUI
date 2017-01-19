using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupElementSizeSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            Vector2 pos;
            if (!xml.HasVector2("Size", out pos)) return;

            var position = entity.Need<Size>();
            position.Value = pos;
            entity.ReplaceInstance(position);
        }
    }
}
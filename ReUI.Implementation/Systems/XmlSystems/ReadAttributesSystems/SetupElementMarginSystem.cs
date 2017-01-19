using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupElementMarginSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            Vector4 marg;
            if (!xml.HasVector4("Margin", out marg))
                return;

            var margins = entity.Need<Margin>();
            margins.Value = marg;
            entity.ReplaceInstance(margins);
        }
    }
}
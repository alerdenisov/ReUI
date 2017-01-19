using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Implementation
{

    public class SetupElementPivotSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            if (!xml.HasAttribute("Pivot")) return;
            Vector2 pos;
            PivotType pivotType;

            if (xml.HasVector2("Pivot", out pos))
            {

            }
            else if (xml.HasEnum("Pivot", out pivotType))
            {
                pos = PivotTypeVector2Converter.Convert(pivotType);
            }

            var position = entity.Need<Pivot>();
            position.Value = pos;
            entity.ReplaceInstance(position);
        }
    }
}
using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupTextAlignmentSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (TextType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            TextAnchor anchor;
            if(!xml.HasEnum("Alignment", out anchor))
                anchor = TextAnchor.UpperLeft;

            var textAlignment = entity.Need<Api.TextAlignment>();
            textAlignment.Value = anchor;
            entity.ReplaceInstance(textAlignment);

        }
    }
}
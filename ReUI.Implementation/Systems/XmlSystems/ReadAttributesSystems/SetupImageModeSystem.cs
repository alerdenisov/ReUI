using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine.UI;

namespace ReUI.Implementation
{
    public class SetupImageModeSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new[] {typeof (SpriteType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            Image.Type mode;
            if (!xml.HasEnum("Mode", out mode))
                mode = Image.Type.Simple;

            var split =entity.Need<SplitMode>();
            split.Value = mode;
            entity.ReplaceInstance(split);
        }
    }
}
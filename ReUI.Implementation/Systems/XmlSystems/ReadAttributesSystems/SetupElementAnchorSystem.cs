using System.Collections.Generic;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{

    public class SetupElementAnchorSystem : AbstractSetupAttributeSystem
    {
        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            var isRoot = entity.Has<ScopeType>();
            Vector4 result = entity.Get<Anchor>().Value;

            AnchorType type;
            if (xml.HasEnum("Anchor", out type))
            {
                result = AnchorTypeVectro4Converter.Convert(type);
            }
            else
            {
                LookAtAnchor(xml, ref result);
                LookAtStretching(xml, ref result);
                LookAtAxis(xml, ref result);
            }
            var anchor = entity.Need<Anchor>();
            anchor.Value = result;
            entity.ReplaceInstance(anchor);
        }

        private void LookAtAxis(XmlElement xml, ref Vector4 result)
        {
            LookAtAxisX(xml, ref result);
            LookAtAxisY(xml, ref result);
        }

        private void LookAtAxisX(XmlElement xml, ref Vector4 result)
        {
            Vector2 axis;
            if (xml.HasVector2("AnchorX", out axis))
            {
                result.x = axis.x;
                result.z = axis.y;
            }
        }

        private void LookAtAxisY(XmlElement xml, ref Vector4 result)
        {
            Vector2 axis;
            if (xml.HasVector2("AnchorY", out axis))
            {
                result.y = axis.x;
                result.w = axis.y;
            }
        }

        private void LookAtStretching(XmlElement xml, ref Vector4 result)
        {
            LookAtStretchX(xml, ref result);
            LookAtStretchY(xml, ref result);
        }

        private void LookAtStretchY(XmlElement xml, ref Vector4 result)
        {
            if (xml.HasFlag("StretchHeight"))
            {
                result.y = 0f;
                result.w = 1f;
            }
        }

        private void LookAtStretchX(XmlElement xml, ref Vector4 result)
        {
            if (xml.HasFlag("StretchWidth"))
            {
                result.x = 0f;
                result.z = 1f;
            }
        }

        private void LookAtAnchor(XmlElement xml, ref Vector4 result)
        {
            if (!xml.Attributes.ContainsKey("Anchor"))
                return;

            Vector4Converter.Convert(xml.Attributes["Anchor"], ref result);
        }
    }
}
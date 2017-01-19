using System;
using System.Collections.Generic;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class SetupElementLuaCodeSystem : AbstractSetupAttributeSystem
    {
        public static Dictionary<string, Action<Entity<IUIPool>, XmlElement>> _luaProperties = new Dictionary<string, Action<Entity<IUIPool>, XmlElement>>
        {
            { "OnCreate", CreateAttribute },
            { "OnDestroy", DestroyAttribute },
            { "OnTick", TickAttribute },
            { "OnProps", PropsAttribute },
            { "OnState", StateAttribute },
            { "OnMouseOver", MouseOverAttribute },
            { "OnMouseOut", MouseOutAttribute },
            { "OnMouseRelease", ReleaseAttribute },
            { "OnMousePress", PressAttribute},
            { "OnClick", ClickAttribute},
            { "Itteration", ItterationAttribute },
            { "Collection", CollectionAttribute },
        };

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            bool hasLua = false;
            foreach (var kv in _luaProperties)
            {
                if (!xml.HasAttribute(kv.Key)) continue;
                _luaProperties[kv.Key](entity, xml);
                hasLua = true;
            }

            if (hasLua)
            {
                entity.Add<LuaCode>(code => code.Value.Reset());
                entity.Toggle<LuaType>(true);
            }

        }

        private static void ClickAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnClick>(entity, xml.Attributes["OnClick"]);
        }

        private static void PressAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnPress>(entity, xml.Attributes["OnMousePress"]);
        }

        private static void ReleaseAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnRelease>(entity, xml.Attributes["OnMouseRelease"]);
        }

        private static void MouseOutAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnMouseOut>(entity, xml.Attributes["OnMouseOut"]);
        }

        private static void MouseOverAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnMouseOver>(entity, xml.Attributes["OnMouseOver"]);
        }

        private static void PropsAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnProps>(entity, xml.Attributes["OnProps"]);
        }

        private static void StateAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnState>(entity, xml.Attributes["OnState"]);
        }

        private static void TickAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnTick>(entity, xml.Attributes["OnTick"]);
        }

        private static void DestroyAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnDestroy>(entity, xml.Attributes["OnDestroy"]);
        }

        private static void CreateAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeOnCreate>(entity, xml.Attributes["OnCreate"]);
        }

        private static void ItterationAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeLoopItteration>(entity, xml.Attributes["Itteration"]);
        }

        private static void CollectionAttribute(Entity<IUIPool> entity, XmlElement xml)
        {
            AddLuaAttribute<LuaCodeLoopCollection>(entity, xml.Attributes["Collection"]);
        }

        private static void AddLuaAttribute<T>(Entity<IUIPool> entity, string code)
            where T : class, IAttributeValue<string>, ILuaComponent, new()
        {
            entity.SetAttribute<T, string>(code);
        }
    }
}
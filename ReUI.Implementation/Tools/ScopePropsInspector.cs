using System;
using System.Collections.Generic;
using System.Linq;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Tools
{
    public static class ScopePropsInspector  
    {
        private static readonly Dictionary<Type, string> TypeNames = new Dictionary<Type, string>();

        public static void InspectorGUI(this LuaScopeProps props)
        {
            props.DrawLuaTable();
        }

        public static void InspectorGUI(this LuaCompiled props)
        {
            props.DrawLuaTable();
        }

        public static void InspectorGUI(this LuaScopeState state)
        {
            state.DrawLuaTable();
        }

        private static void DrawLuaTable(this IAttributeValue<ILuaTable> props)
        {
            GUILayout.Box(TypeName(props.GetType()) + " " + props.Value.ToString());
            foreach (var key in props.Value.GetKeys())
            {
                GUILayout.BeginHorizontal();
                {
                    var value = props.Value[key];
                    GUILayout.Label(key.ToString(), GUILayout.Width(80));
                    GUILayout.Label(TypeName(value.GetType()), GUILayout.Width(100));
                    GUILayout.Label(value.ToString());
                }
                GUILayout.EndHorizontal();
            }
        }

        private static string TypeName(Type type)
        {
            if (type == null) return "Null";

            if (!TypeNames.ContainsKey(type))
            {
                TypeNames.Add(type, type.ToString().Split('.').Last());
            }

            return TypeNames[type];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Tools
{
    public static class XMLElementInspector
    {
        private static readonly Dictionary<Type, string> TypeNames = new Dictionary<Type, string>();

        public static void InspectorGUI(this XmlElement props)
        {
            DrawField("Name", props.Name);

            if(!string.IsNullOrEmpty(props.Content))
                GUILayout.Box(props.Content.Trim());

            GUILayout.BeginVertical(GUI.skin.box);
            {
                foreach (var kv in props.Attributes)
                {
                    DrawField(kv.Key, kv.Value);
                }
            }
            GUILayout.EndVertical();
        }

        private static void DrawField(string label, object value, string dump = null)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label, GUILayout.Width(150));
                if (value != null)
                {
                    GUILayout.Label(TypeName(value.GetType()), GUILayout.Width(100));
                    GUILayout.Label(dump ?? value.ToString());
                }
                else
                {
                    GUILayout.Label("Null");
                }
            }
            GUILayout.EndHorizontal();
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
using System.Collections;
using System.Collections.Generic;
using ReUI.Api;
using UnityEngine;
using UnityEditor;
using ReUI.Integrations.View;

[CustomEditor(typeof(SimpleView))]
public class SimpleViewInspector : Editor {
    public override void OnInspectorGUI()
    {
        var view = (SimpleView) target;
        if (view.Owner != null)
        {
            var components = view.Owner.GetComponents();
            foreach (var component in components)
            {
                var type = component.GetType();
                if (type == typeof (XmlElement))
                    DrawXmlElement(component);
                else if (type == typeof (LuaScopeProps))
                    DrawProps(component);
                else
                    DrawGeneric(component);
            }
            
        }
        base.OnInspectorGUI();
    }

    private void DrawProps(IUIPool component)
    {
        BeginPanel("Scope Props");
        {
            var props = (LuaScopeProps) component;
            var table = props.Value;
            if (table == null) return;

            foreach (var key in table.GetKeys())
            {
                DrawField(key.ToString(), table[key].ToString());
            }
        }
        EndPanel();
    }

    private void DrawGeneric(IUIPool component)
    {
        GUILayout.Box(component.GetType().FullName);
    }

    private void DrawXmlElement(IUIPool component)
    {
        BeginPanel("Xml Element");
        var xml = (XmlElement) component;
        GUILayout.Label($"<{xml.Name} />", EditorStyles.boldLabel);
        if(!string.IsNullOrEmpty(xml.Content))
            DrawField("Content", xml.Content);

        foreach (var kv in xml.Attributes)
            DrawField(kv.Key, kv.Value);

        EndPanel();
    }

    private static void DrawField(string label, string content)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label,   GUILayout.Width(EditorGUIUtility.labelWidth));
            var left = new GUIStyle(GUI.skin.box);
            left.alignment = TextAnchor.UpperLeft;
            GUILayout.Box(content, left, GUILayout.ExpandWidth(true));
        }
        GUILayout.EndHorizontal();
    }

    private static void EndPanel()
    {
        GUILayout.EndVertical();
    }

    private static void BeginPanel(string name)
    {
        GUILayout.Space(5);
        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Button(name);
    }
}

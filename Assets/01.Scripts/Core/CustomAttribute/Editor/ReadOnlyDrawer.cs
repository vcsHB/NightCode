using UnityEngine;
using UnityEditor;
using System;

namespace Core.Attribute
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, true);
            EditorGUI.EndDisabledGroup();
        }
    }
}

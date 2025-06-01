using UnityEditor;
using UnityEngine;

namespace Core.Attribute
{

    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProp = property.serializedObject.FindProperty(showIf.ConditionFieldName);

            if (conditionProp != null && conditionProp.propertyType == SerializedPropertyType.Boolean && conditionProp.boolValue)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProp = property.serializedObject.FindProperty(showIf.ConditionFieldName);

            if (conditionProp != null && conditionProp.propertyType == SerializedPropertyType.Boolean && conditionProp.boolValue)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return 0f; // Disable
        }
    }

}
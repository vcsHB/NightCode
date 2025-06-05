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

            if (ShouldShow(conditionProp, showIf))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProp = property.serializedObject.FindProperty(showIf.ConditionFieldName);

            if (ShouldShow(conditionProp, showIf))
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return 0f;
        }

        private bool ShouldShow(SerializedProperty conditionProp, ShowIfAttribute showIf)
        {
            if (conditionProp == null || conditionProp.propertyType != SerializedPropertyType.Boolean)
            {
                Debug.LogWarning($"[ShowIf] Condition field '{showIf.ConditionFieldName}' not found or not a bool.");
                return true; // default to showing
            }

            bool value = conditionProp.boolValue;
            return showIf.Invert ? !value : value;
        }
    }

}
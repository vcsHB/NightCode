using UnityEditor;
using UnityEngine;

namespace Core.Attribute
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ConditionAttribute))]
    public class ConditionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CheckCondition(property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return CheckCondition(property) ? EditorGUI.GetPropertyHeight(property, label, true) : 0f;
        }

        private bool CheckCondition(SerializedProperty property)
        {
            ConditionAttribute conditionAttribute = attribute as ConditionAttribute;
            SerializedProperty compare = property.serializedObject.FindProperty(conditionAttribute.FieldName);

            string path = property.propertyPath;
            if (path.Contains('.'))
            {
                string parentPath = path.Substring(0, path.LastIndexOf('.'));
                compare = property.serializedObject.FindProperty($"{parentPath}.{conditionAttribute.FieldName}");
            }

            bool enabled = false;

            if (compare != null)
            {
                switch (compare.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        enabled = compare.boolValue == (bool)conditionAttribute.ToCompare;
                        break;
                    case SerializedPropertyType.Enum:
                        enabled = compare.enumValueIndex == (int)conditionAttribute.ToCompare;
                        break;
                    default:
                        Debug.LogWarning($"Condition attribute not supported for property type: {compare.propertyType}");
                        break;
                }
            }

            return enabled;
        }
    }

}

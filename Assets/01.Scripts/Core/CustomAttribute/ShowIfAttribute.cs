using UnityEngine;

namespace Core.Attribute
{

    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionFieldName;

        public ShowIfAttribute(string conditionFieldName)
        {
            this.ConditionFieldName = conditionFieldName;
        }
    }
}
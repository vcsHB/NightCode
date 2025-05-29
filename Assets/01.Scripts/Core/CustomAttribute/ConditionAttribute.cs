using System;
using UnityEngine;

namespace Core.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConditionAttribute : PropertyAttribute
    {
        public string FieldName { get; private set; }
        public object ToCompare { get; private set; }

        public ConditionAttribute(string fieldName, object toCompare = null)
        {
            FieldName = fieldName;
            ToCompare = toCompare;
        }
    }
}

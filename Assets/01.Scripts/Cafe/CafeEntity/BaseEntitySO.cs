using Base.Cafe;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    [CreateAssetMenu(menuName = "SO/Base/Entity")]
    public class BaseEntitySO : ScriptableObject
    {
        public float moveSpeed;
        public List<BaseEntityStateSO> entityStateList;
    }
}

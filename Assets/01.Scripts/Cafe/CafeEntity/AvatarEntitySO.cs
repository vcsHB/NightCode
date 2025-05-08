using Base.Cafe;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    [CreateAssetMenu(menuName = "SO/Base/Entity")]
    public class AvatarEntitySO : ScriptableObject
    {
        public float moveSpeed;
        public List<AvatarEntityStateSO> entityStateList;
    }
}

using Agents.Animate;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/Entity")]
    public class CafeEntitySO : ScriptableObject
    {
        public float moveSpeed;
        public List<CafeEntityStateSO> entityStateList;
    }
}

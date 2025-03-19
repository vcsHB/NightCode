using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/Condition/CoinCondition")]
    public class CoinCondition : ConditionSO
    {
        [Header("더 적으면 False, 더 많거나 같으면 True")]
        public int coinLess;

        public override bool Decision() => DialogConditionManager.Instance.coin <= coinLess;
    }
}

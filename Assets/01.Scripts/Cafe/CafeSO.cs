using Office;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(fileName = "CafeSO", menuName = "SO/Cafe/CafeSO")]
    public class CafeSO : ScriptableObject
    {
        public float openTime;
        public List<CafeCustomerWaveSO> customerWave;
        public MissionSO mission;
    }
}

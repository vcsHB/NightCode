using Core.StageController;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    [CreateAssetMenu(fileName = "CafeSO", menuName = "SO/Cafe/CafeSO")]
    public class CafeSO : ScriptableObject
    {
        public float openTime;
        public List<CafeCustomerWaveSO> customerWave;
        public StageSO mission;
    }
}

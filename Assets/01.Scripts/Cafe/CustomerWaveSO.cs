using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Cafe/WaveSO")]
    public class CustomerWaveSO : ScriptableObject
    {
        [Tooltip("소환될 손님의 종류")]
        public List<CafeCustomerSO> customerToSpawn;

        [Tooltip("손님을 얼마나 소환할지(x 와 y 값 사이)")]
        public int spawnValue;

        [Tooltip("손님 스폰 간격(x 와 y 값 사이)")]
        public int spawnDelay;

        [Tooltip("이전 웨이브가 끝나고 얼마나 뒤에 실행될 건지")]
        public float waveDelay;
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CustomerSO")]
    public class CafeCustomerSO : ScriptableObject
    {
        public CafeCustomer customerPf;
        public Sprite customerIcon;
        public float moveSpeed;
        public float menuWaitingTime;

        [Tooltip("오므라이스 그림 요구할 확률 100분률")]
        public int minigameRequireChance;
        [Tooltip("오므라이스 그림")]
        public List<string> miniGamePainting;

        public string reviewOnGood;
        public string reviewOnBad;

        public string GetRandomPainingName()
            => miniGamePainting[Random.Range(0, miniGamePainting.Count)];
    }
}

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
        [Tooltip("오므라이스 그림에서 요구하는 점수")]
        public int requireMinigamePoint;

        public string reviewOnGood;
        public string reviewOnBad;
    }
}

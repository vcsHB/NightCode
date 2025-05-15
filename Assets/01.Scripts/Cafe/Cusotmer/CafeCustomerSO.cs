using Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CustomerSO")]
    public class CafeCustomerSO : ScriptableObject
    {
        public DialogSO talk;
        public CafeCustomer customerPf;
        public float moveSpeed;
        public bool isClient;
        public bool isInteractiveCustomer;

        [Tooltip("오므라이스 그림")]
        public List<string> miniGamePainting;

        public string GetRandomPainingName()
            => miniGamePainting[Random.Range(0, miniGamePainting.Count)];
    }
}

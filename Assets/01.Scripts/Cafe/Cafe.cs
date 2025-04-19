using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    public class Cafe : MonoBehaviour
    {
        public Transform customerInitPosition;
        public Transform tableParent;

        private CafeSO _cafeInfo;
        private List<CafeTable> _tableList;
        private int _currentIndex = 0;

        private void Awake()
        {
            _tableList = new List<CafeTable>();
            for (int i = 0; i < tableParent.childCount; i++)
            {
                if (tableParent.GetChild(i).TryGetComponent(out CafeTable table))
                    _tableList.Add(table);
            }
        }

        public void Init(CafeSO cafeSO)
        {
            _cafeInfo = cafeSO;
        }

        public void EnterNextCustomer()
        {
            if (_currentIndex >= _cafeInfo.customerToCome.Count) return;

            CafeCustomerSO cafeCustomerSO = _cafeInfo.customerToCome[_currentIndex++];
            SpawnCustomer(cafeCustomerSO);
        }


        private bool SpawnCustomer(CafeCustomerSO customerSO)
        {
            if (TryGetValiadeTable(out CafeTable table))
            {
                CafeCustomer customer = Instantiate(customerSO.customerPf, customerInitPosition);
                customer.onExitCafe += EnterNextCustomer;
                customer.Init(table, customerSO.talk);
                return true;
            }
            return false;
        }

        public bool TryGetValiadeTable(out CafeTable table)
        {
            int randomIndex = Random.Range(0, _tableList.Count);
            table = _tableList[randomIndex];
            return true;
        }
    }
}

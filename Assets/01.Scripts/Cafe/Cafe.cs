using Core.StageController;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Base.Cafe
{
    public class Cafe : MonoBehaviour
    {
        public Transform customerInitPosition;
        public Transform tableParent;

        private CafeCustomerWaveSO _customerWave;
        private CustomerInfo _customerInfo;
        private CafeSO _cafeInfo;
        private List<CafeSit> _tableList;

        private int _currentIndex = 0;
        private int _currentWaveIndex = 0;
        private int _completCustomer = 0;
        private float _prevSpawnTime;
        private bool _isWaveStart = false;
        private bool _isLastWave = false;

        private void Awake()
        {
            _isLastWave = false;
            _tableList = new List<CafeSit>();
            for (int i = 0; i < tableParent.childCount; i++)
            {
                if (tableParent.GetChild(i).TryGetComponent(out CafeSit table))
                    _tableList.Add(table);
            }
        }

        private void Update()
        {
            if (_isWaveStart)
            {
                if (_currentWaveIndex >= _customerWave.exsistCustomer.Count) return;
                _customerInfo = _customerWave.exsistCustomer[_currentWaveIndex];

                if (_prevSpawnTime + _customerInfo.exsistDelay < Time.time)
                {
                    CafeCustomerSO cafeCustomerSO = _customerInfo.customer;
                    SpawnCustomer(cafeCustomerSO, ((_currentWaveIndex + 1) >= _customerWave.exsistCustomer.Count));
                    _prevSpawnTime = Time.time;
                    _currentWaveIndex++;
                }
            }
        }

        public void Init(CafeSO cafeSO)
        {
            _cafeInfo = cafeSO;
        }

        public void StartCustomerWave()
        {
            _completCustomer++;
            if (_currentIndex >= _cafeInfo.customerWave.Count)
            {
                _isLastWave = true;
                return;
            }
            if (_customerWave != null && _completCustomer < _customerWave.exsistCustomer.Count) return;

            _isWaveStart = true;
            _currentWaveIndex = 0;
            _completCustomer = 0;
            _prevSpawnTime = Time.time;
            _customerWave = _cafeInfo.customerWave[_currentIndex++];
        }


        private bool SpawnCustomer(CafeCustomerSO customerSO, bool isLastCustomer)
        {
            if (TryGetValiadeTable(out CafeSit table))
            {
                CafeCustomer customer = Instantiate(customerSO.customerPf, customerInitPosition);
                customer.onExitCafe += StartCustomerWave;
                customer.Init(table, customerSO.talk);
                
                if (_isLastWave && isLastCustomer)
                {
                    customer.onExitCafe += () =>
                    {
                        //TODO: Before change scene have to show UI
                        StageManager.Instance.LoadNextStage();
                    };
                }

                return true;
            }
            return false;
        }

        public bool TryGetValiadeTable(out CafeSit sit)
        {
            var sitListTemp = _tableList;

            for (int i = 0; i < sitListTemp.Count; i++)
            {
                int randomIndex = Random.Range(0, sitListTemp.Count);
                CafeSit sitTemp = sitListTemp[i];
                sitListTemp[i] = sitListTemp[randomIndex];
                sitListTemp[randomIndex] = sitTemp;
            }

            for (int i = 0; i < sitListTemp.Count; i++)
            {
                if (sitListTemp[i].AssingedCustomer == null)
                {
                    sit = sitListTemp[i];
                    return true;
                }
            }

            sit = null;
            return false;
        }
    }
}

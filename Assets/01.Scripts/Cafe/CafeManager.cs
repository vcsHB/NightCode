
using Basement;
using Office;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Cafe
{
    public class CafeManager : MonoSingleton<CafeManager>
    {
        public CafeSO cafeSO;
        public CafeInput input;
        public MSGText msgText;
        public ResultPanel resultPanel;
        public OmeletRiceMiniGame omeletRiceMiniGame;
        public Cafe cafe;

        //ForDebuging
        [SerializeField] private MissionSO missionSO;

        private int _totalCustomer;
        private int _waveIndex = 0;
        private float _prevWaveTime = 0;
        private float _nextSpawnTime = 0;
        private bool _isCustomerSpawning = false;
        private CustomerWaveSO _currentWave;

        private int _customerToSpawn;
        private List<(CafeCustomerSO customer, int rating)> _visitedCustomer = new();

        public bool IsCafeOpen { get; private set; }
        public float CurrentTime { get; private set; }

        protected override void Awake()
        {
            input.DisableInput();
        }

        private void Update()
        {
            if (IsCafeOpen)
            {
                CurrentTime += Time.deltaTime;
                TrySpawnCustomer();

                if (CurrentTime > cafeSO.openTime) CloseCafe();
            }
        }


        #region CafeFlow

        public void StartCafe()
        {
            IsCafeOpen = true;
            input.EnableInput();
            _currentWave = cafeSO.waveData[_waveIndex];
            _customerToSpawn = _currentWave.spawnValue;
        }

        private void CloseCafe()
        {
            IsCafeOpen = false;

            int customerCount = _totalCustomer;
            int rating = 0;

            _visitedCustomer.ForEach(customer => rating += customer.rating);
            float ratingAvg = Mathf.RoundToInt((float)rating / (float)customerCount);
            rating += (int)(ratingAvg - 1) * (_totalCustomer - _visitedCustomer.Count);

            rating = Mathf.RoundToInt((float)rating / (float)customerCount);
            Debug.Log(rating);

            resultPanel.Open();
            resultPanel.Init(missionSO, _visitedCustomer.Count, rating);
            //정산 같은거를 해야겠지
        }

        private void TrySpawnCustomer()
        {
            if (_isCustomerSpawning)
            {
                if (_nextSpawnTime < CurrentTime)
                {
                    SpawnCustomer();

                    if (_customerToSpawn <= 0)
                    {
                        _prevWaveTime = CurrentTime;
                        _isCustomerSpawning = false;

                        if (++_waveIndex < cafeSO.waveData.Count)
                        {
                            _currentWave = cafeSO.waveData[_waveIndex];
                        }
                        else
                        {
                            //존재하는 모든 웨이브를 수행하면 손님이 안오게 끝나는 시간으로 바꿔
                            _prevWaveTime = cafeSO.openTime;
                        }
                    }
                }
            }
            else
            {
                if (CurrentTime > _prevWaveTime + _currentWave.waveDelay)
                {
                    _isCustomerSpawning = true;
                    SpawnCustomer();
                }
            }
        }

        private void SpawnCustomer()
        {
            CafeCustomerSO customerSO = _currentWave.customerToSpawn[Random.Range(0, _currentWave.customerToSpawn.Count)];
            bool isMissCustomer = cafe.EnterCustomer(customerSO);

            _nextSpawnTime = CurrentTime + _currentWave.spawnDelay;
            _customerToSpawn--;
            _totalCustomer++;
        }

        #endregion

        public void AddReview(CafeCustomerSO customer, int rating)
        {
            rating = Mathf.Clamp(rating, 1, 5);
            _visitedCustomer.Add((customer, rating));

            string review = rating > 3 ? customer.reviewOnGood : customer.reviewOnBad;
            msgText.PopMSGText(customer.customerIcon, review, rating);
        }
    }
}

using System;
using UnityEngine;

namespace Cafe
{
    public class Kitchen : CafeInteractiveObject
    {
        public float foodDelay = 1;
        public GameObject foodObjectIcon;
        public ProgressBar progressBar;
        //Debuging
        public FoodSO food;

        private float _prevFoodOutTime;
        private bool _isFoodOut = false;

        private void Update()
        {
            if (!_isFoodOut && _prevFoodOutTime + foodDelay < Time.time)
            {
                _isFoodOut = true;
                foodObjectIcon.SetActive(true);
                progressBar.gameObject.SetActive(false);
            }
            else if (_prevFoodOutTime + foodDelay >= Time.time)
            {
                float endTime = _prevFoodOutTime + foodDelay;
                progressBar.SetFillAmount((endTime - Time.time) / foodDelay);
                progressBar.SetText($"{Math.Round(endTime - Time.time, 1)}s");
                progressBar.gameObject.SetActive(true);
            }
        }

        public FoodSO GetFood()
        {
            if (_isFoodOut == false) return null;

            _isFoodOut = false;
            foodObjectIcon.SetActive(false);
            _prevFoodOutTime = Time.time;
            return food;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player) == false) return;
            if (_isFoodOut == false || _player.isGetFood) return;
            base.OnTriggerEnter2D(collision);
        }
    }
}

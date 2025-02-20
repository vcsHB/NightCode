using UnityEngine;

namespace ObjectManage.Obstacles
{

    public class Scaffolding : Obstacle
    {
        [SerializeField] private float _supportingPower;
        [SerializeField] private float _recoverPower = 2f;

        private float _currentLoadMass;
        private bool _isOnFloor;

        void OnCollisionEnter2D(Collision2D collision)
        {
            _isOnFloor = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _isOnFloor = false;
        }
        private void Update()
        {
            if (_isOnFloor)
                _currentLoadMass += Time.deltaTime;
            else
                _currentLoadMass -= Time.deltaTime * _recoverPower;

            if (_currentLoadMass >= _supportingPower)
            {
                HandleDieEvent();
            }
        }





    }


}
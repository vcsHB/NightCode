using Basement.CameraController;
using UnityEngine;

namespace Basement
{
    public class BasementPlayerFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _playerTrm;
        private float _speed = 8;

        private void Update()
        {

            if (BasementCameraManager.Instance.CameraMode == CameraMode.Build) return;
            
            transform.position 
                = new Vector3(_playerTrm.position.x, transform.position.y, transform.position.z);
        }


        public void Move(float dir)
        {
            transform.position += Vector3.right * _speed * dir * Time.deltaTime;
        }
    }
}

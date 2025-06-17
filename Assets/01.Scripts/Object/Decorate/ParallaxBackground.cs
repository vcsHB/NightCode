using UnityEngine;

namespace ObjectManage.DecorateObjects
{

    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _parallaxMultiplier = 0.5f;

        private Vector3 _previousCameraPosition;

        private void Start()
        {
            if (_cameraTransform == null)
                _cameraTransform = Camera.main.transform;

            _previousCameraPosition = _cameraTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 delta = _cameraTransform.position - _previousCameraPosition;
            transform.position += new Vector3(delta.x * _parallaxMultiplier, delta.y * _parallaxMultiplier, 0);
            _previousCameraPosition = _cameraTransform.position;
        }
    }

}
using UnityEngine;

namespace ObjectManage.DecorateObjects
{

    public class FanDecorator : MonoBehaviour
    {
        [SerializeField] private Transform _fanTrm;
        [SerializeField] private float _spinSpeed = 0.4f;
        [SerializeField] private float _rotateOffset;


        private void Update()
        {
            _fanTrm.rotation = Quaternion.Euler(0, 0, _fanTrm.rotation.eulerAngles.z + _spinSpeed + _rotateOffset);
        }
    }

}
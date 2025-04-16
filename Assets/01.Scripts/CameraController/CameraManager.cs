using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

namespace CameraControllers
{

    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera _camera;
        private Dictionary<Type, ICameraControlable> _controllers = new Dictionary<Type, ICameraControlable>();
        public Transform CurrentFollowTarget => _camera.Follow;
        [SerializeField] private Transform _defaultFollowTarget;
        private CinemachineFollow _followCam;
        private Vector3 _defaultFollowOffset;

        protected override void Awake()
        {
            base.Awake();

            _followCam = _camera.GetComponent<CinemachineFollow>();
            _defaultFollowOffset = _followCam.FollowOffset;
            GetComponentsInChildren<ICameraControlable>(true)
               .ToList().ForEach(controller => _controllers.Add(controller.GetType(), controller));
            foreach (ICameraControlable controller in _controllers.Values)
            {
                controller.Initialize(_camera);
            }
        }


        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_controllers.TryGetValue(typeof(T), out ICameraControlable compo))
            {
                return compo as T;
            }

            if (!isDerived) return default;

            Type findType = _controllers.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _controllers[findType] as T;

            return default(T);
        }

        public void SetFollow(Transform target)
        {
            _camera.Follow = target;
        }

        public void ResetFollow()
        {
            if (_defaultFollowTarget == null) return;
            _camera.Follow = _defaultFollowTarget;
        }

        public void SetFollowOffset(Vector2 newOffset)
        {
            Vector3 offset = (Vector3)newOffset;
            offset.z = -10f;
            _followCam.FollowOffset = offset;
        }
        public void ResetFollowOffset()
        {
            _followCam.FollowOffset = _defaultFollowOffset;
        }




    }
}
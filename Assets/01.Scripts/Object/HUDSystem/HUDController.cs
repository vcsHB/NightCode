using UnityEngine;

namespace HUDSystem
{

    public class HUDController : MonoSingleton<HUDController>
    {
        [SerializeField] private Transform _followTarget;
        private HUDObject[] _hudList;

        private bool _isFollow = true;

        protected override void Awake()
        {
            base.Awake();
            _hudList = GetComponentsInChildren<HUDObject>();
        }

        private void Update()
        {

            Follow();
        }

        public void SetFollowTarget(Transform newTarget)
        {
            _followTarget = newTarget;
        }
        private void Follow()
        {
            if (!_isFollow) return;

            transform.position = _followTarget.position;

        }

        public T GetHUD<T>() where T : class
        {
            foreach (var hud in _hudList)
            {
                if (hud is T)
                {
                    return hud as T;
                }
            }
            return default(T);
        }
    }

}
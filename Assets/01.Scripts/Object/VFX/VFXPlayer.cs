using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{

    public class VFXPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] private float _lifeTime = 2f;

        public void Play()
        {
            _vfx.Play();
            Invoke(nameof(HandleOverLifeTime), _lifeTime);
        }

        private void HandleOverLifeTime()
        {
            PoolManager.Instance.Push(this);
        }

        public void ResetItem()
        {
            _vfx.Clear();
        }
    }

}
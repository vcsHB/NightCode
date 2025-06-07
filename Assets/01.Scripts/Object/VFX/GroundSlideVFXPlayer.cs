using ObjectPooling;
using UnityEngine;
namespace ObjectManage.VFX
{

    public class GroundSlideVFXPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] private float _lifeTime = 2f;

        public void Play(float xDirection)
        {
            _vfx.Play();
            _vfx.transform.localScale = new Vector3(xDirection, 1f, 1f);
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
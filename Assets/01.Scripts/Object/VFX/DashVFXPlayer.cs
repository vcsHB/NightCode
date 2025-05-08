using Agents;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage.VFX
{

    public class DashVFXPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform _visualTrm;
        [SerializeField] private float _yEnableDuration;
        [SerializeField] private float _xEnableDuration;
        [SerializeField] private float _disableDuration;
        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;

        [ContextMenu("DebugPlay")]
        private void DebugPlay()
        {
            Play(Vector2.zero, Vector2.left, 10f);
        }

        public void Play(Vector2 startPosition, Vector2 direction, float power)
        {
            transform.position = startPosition;
            _visualTrm.right = -direction.normalized;

            _visualTrm.localScale = new Vector3(0f, 1f, 1f);
            Sequence sequecne = DOTween.Sequence();
            sequecne.Append(_visualTrm.DOScaleX(power, _xEnableDuration));
            sequecne.Append(_visualTrm.DOScaleY(0f, _disableDuration));
            sequecne.OnComplete(() => PoolManager.Instance.Push(this));

        }

        public void ResetItem()
        {
        }
    }

}
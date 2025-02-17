using UnityEngine;
using UnityEngine.Events;
namespace Combat
{
    public class CasterData
    {
        // Data Capsule Grouping Class
    }

    public abstract class Caster : MonoBehaviour
    {
        [Tooltip("Calls by 1 Cast")]
        public UnityEvent OnCastEvent;
        [Tooltip("Calls when Successed Cesting")]
        public UnityEvent OnCastSuccessEvent;

        [Space(10f)]
        [Header("Setting Values")]
        [SerializeField] protected Vector2 _offset;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _targetMaxAmount;
        protected ICastable[] _casters;
        protected Collider2D[] _hits;
        public Vector2 CenterPosition => (Vector2)transform.position + _offset;
        [Header("Gizmos Setting")]
        [SerializeField] protected Color _gizmosColor = Color.red;

        protected virtual void Awake()
        {
            _casters = GetComponentsInChildren<ICastable>();
        }

        public virtual void Cast()
        {
            OnCastEvent?.Invoke();
        }

        public void ForceCast(Collider2D[] hit)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                for (int j = 0; j < _casters.Length; j++)
                {
                    _casters[j].Cast(hit[i]);
                }
                OnCastSuccessEvent?.Invoke();
            }
        }

        public void ForceCast(Collider2D hit)
        {
            for (int j = 0; j < _casters.Length; j++)
            {
                _casters[j].Cast(hit);
            }
            OnCastSuccessEvent?.Invoke();
        }

        public void SendCasterData(CasterData data)
        {
            foreach (ICastable caster in _casters)
            {
                caster.HandleSetData(data);
            }
        }
    }
}
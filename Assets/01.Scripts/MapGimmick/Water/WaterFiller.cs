using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ingame.Gimmick
{
    [System.Serializable]
    public struct WaterFillSequence
    {
        public float fillLevel;
        public float fillDuration;
        public float term;
        public Color gizmosColor;

    }
    public class WaterFiller : MonoBehaviour
    {
        public UnityEvent OnFillCompletedEvent;
        public UnityEvent OnAllFillCompleteEvent;
        [SerializeField] private DamageWater _water;

        [SerializeField] private float gizmosWide = 100f;
        [SerializeField] private WaterFillSequence[] _sequence;
        private WaterFillSequence CurrentSequence => _sequence[_currentSequenceLevel];
        [SerializeField] private int _currentSequenceLevel = -1; // -1
        private float _defaultFillLevel;
        private bool _isFilling;

        private void Start()
        {
            _defaultFillLevel = _water.CurrentFillLevel;
        }

        [ContextMenu("DebugFillSequenceStart")]
        public void StartFillSequence()
        {

            StartCoroutine(SequenceCoroutine());
        }
        private IEnumerator SequenceCoroutine()
        {
            for (int i = _currentSequenceLevel + 1; i < _sequence.Length; i++)
            {
                yield return StartFill();
                yield return new WaitForSeconds(_sequence[i].term);
            }
        }

        [ContextMenu("DebugFillStart")]
        public Coroutine StartFill()
        {
            if (_isFilling || _currentSequenceLevel >= _sequence.Length) return null;
            _isFilling = true;
            _currentSequenceLevel++;
            return StartCoroutine(FillCoroutine());
        }

        private IEnumerator FillCoroutine()
        {
            float currentTime = 0;
            float startFill = _water.CurrentFillLevel;
            float endFill = CurrentSequence.fillLevel;
            float duration = CurrentSequence.fillDuration;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                _water.SetFill(Mathf.Lerp(startFill, endFill, currentTime / duration));
                yield return null;
            }
            _isFilling = false;
            OnFillCompletedEvent?.Invoke();
            if (_currentSequenceLevel >= _sequence.Length)
                OnAllFillCompleteEvent?.Invoke();
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (_sequence == null) return;

            for (int i = 0; i < _sequence.Length; i++)
            {
                Gizmos.color = _sequence[i].gizmosColor;
                Vector3 center = transform.position + new Vector3(0f, _sequence[i].fillLevel / 2);
                Vector2 size = new Vector2(gizmosWide, _sequence[i].fillLevel);
                Gizmos.DrawWireCube(center, size);
            }
        }
#endif
    }

}
using System.Collections;
using System.Linq.Expressions;
using Agents;
using UnityEngine;
using UnityEngine.Events;
namespace Combat
{
    public class ParryBody : MonoBehaviour, IParryable
    {
        public UnityEvent OnParryEvent;
        public UnityEvent OnCanParryEvent;
        [SerializeField] private float _parryResistance;
        private Agent _owner;
        private bool _canParry;
        private ParryData _parryData;

        private void Awake()
        {
            _owner = GetComponent<Agent>();
        }

        public bool Parry(ParryData data)
        {
            if (!_canParry) return false;
            _parryData = data;
            SetCanParry(false);
            float totalStunDuration = data.stunPower;
            StartCoroutine(ParryedCoroutine(totalStunDuration));
            return true;
        }


        private IEnumerator ParryedCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);

        }


        public void SetCanParry(bool value)
        {
            if (value)
                OnCanParryEvent?.Invoke();


        }
    }
}
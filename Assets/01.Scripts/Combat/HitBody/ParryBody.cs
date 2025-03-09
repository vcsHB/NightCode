using System.Collections;
using System.Linq.Expressions;
using Agents;
using UnityEngine;
using UnityEngine.Events;
namespace Combat
{
    public class ParryBody : MonoBehaviour, IParryable
    {
        public UnityEvent OnParrySuccessEvent;
        public UnityEvent OnCanParryEvent;
        [SerializeField] private float _parryResistance;
        private Agent _owner;
        private bool _canParry;
        private ParryData _parryData;

        private void Awake()
        {
            _owner = GetComponent<Agent>();
        }

        /// <summary>
        /// Try To Parry Method
        /// </summary>
        /// <param name="data"></param>
        /// <returns> isSuccess To Parry</returns>
        public bool Parry(ParryData data)
        {
            if (!_canParry) return false;
            _parryData = data;
            SetCanParry(false);
            float totalStunDuration = data.stunPower;
            StartCoroutine(ParryedCoroutine(totalStunDuration));
            OnParrySuccessEvent?.Invoke();
            return true;
        }


        private IEnumerator ParryedCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            // 스턴 해제
            
        }


        public void SetCanParry(bool value)
        {
            if (value)
                OnCanParryEvent?.Invoke();


        }
    }
}
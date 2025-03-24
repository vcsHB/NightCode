using UnityEngine;
using UnityEngine.Events;
namespace Tutorial
{

    public class ShowPointArea : MonoBehaviour
    {
        public UnityEvent OnGoalArrivedEvent;
        private readonly string _targetTag = "Player";
        private bool _isActive;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isActive) return;
            if (collision.CompareTag(_targetTag))
            {
                _isActive = true;
                OnGoalArrivedEvent?.Invoke();
            }
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
namespace Tutorial
{


    public class GoalObject : MonoBehaviour
    {
        public UnityEvent OnGoalArrivedEvent;
        private readonly string _targetTag = "Player";
        private bool _isActive;
        private SpriteRenderer _goalRenderer;

        private void Awake()
        {
            _goalRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isActive) return;
            if (collision.CompareTag(_targetTag))
            {
                _isActive = true;
                OnGoalArrivedEvent?.Invoke();
                SetDisable();
            }
        }

        private void SetDisable()
        {
            _goalRenderer.enabled = false;
        }
        public void SetEnable()
        {
            _goalRenderer.enabled = true;
        }
    }
}
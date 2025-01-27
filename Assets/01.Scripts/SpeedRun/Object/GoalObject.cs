using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedRun
{

    public class GoalObject : MonoBehaviour
    {
        public UnityEvent OnArriveEvent;
        [SerializeField] private Vector2 _checkBoxSize = Vector2.one;
        [SerializeField] private LayerMask _playerLayer;

        private bool _isChecked;


        private void Update()
        {
            CheckGoal();
        }


        public void ResetGoal()
        {
            _isChecked = false;
        }
        
        private void CheckGoal()
        {
            if (_isChecked) return;

            Collider2D collider = Physics2D.OverlapBox(transform.position, _checkBoxSize, 0, _playerLayer);
            if (collider == null) return;

            OnArriveEvent?.Invoke();
            _isChecked = true;

        }

        private void OnDrawGizmos()
        {
            if (_isChecked) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _checkBoxSize);
        }

    }

}
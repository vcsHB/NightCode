using System;
using UnityEngine;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public event Action OnArriveEndPoint;
        [SerializeField] private Transform _startPosTrm;
        [SerializeField] private EndPoint _endPoint;

        public Vector2 StartPos => _startPosTrm.position;


        private void Awake()
        {
            _endPoint.OnArriveEvent.AddListener(HandleArriveEndPoint);
        }

        private void HandleArriveEndPoint()
        {
            OnArriveEndPoint?.Invoke();
        }

        private void Update()
        {
            _endPoint.CheckTargetArrived();

        }

        public void Destroy()
        {
            Destroy(gameObject);
        }



    }
}
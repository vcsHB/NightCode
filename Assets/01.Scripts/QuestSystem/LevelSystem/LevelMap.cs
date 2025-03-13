using System;
using UnityEngine;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public event Action OnArriveEndPoint;
        [SerializeField] private Transform _startPosTrm;
        [SerializeField] private EndPoint _endPoint;
        [SerializeField] private QuestTargetGroup _questTargetGroup;

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

        internal void AddQuestHandler(Action<QuestTargetData> completeHandle)
        {
            _questTargetGroup.AddQuestHandler(completeHandle);
        }



    }
}
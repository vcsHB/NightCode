using System;
using UnityEngine;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public event Action OnArriveEndPoint;
        [SerializeField] private Transform _startPosTrm;

        public Vector2 StartPos => _startPosTrm.position;

        private void HandleArriveEndPoint()
        {
            OnArriveEndPoint?.Invoke();
        }


        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
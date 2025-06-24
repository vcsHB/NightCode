using System;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public UnityEvent OnLevelLoadEvent;
        [SerializeField] private Transform _startPosTrm;

        public Vector2 StartPos => _startPosTrm.position;

        // Call In Awake Timeline
        public void Initialize()
        {
            OnLevelLoadEvent?.Invoke();
        }

    }
}
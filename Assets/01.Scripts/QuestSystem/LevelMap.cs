using System;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public UnityEvent OnLevelLoadEvent;
        [SerializeField] private Transform _startPosTrm;
        [SerializeField] private Collider2D _cameraConfinder;

        public Vector2 StartPos => _startPosTrm.position;
        public Collider2D CameraConfiner => _cameraConfinder;

        // Call In Awake Timeline
        public void Initialize()
        {
            OnLevelLoadEvent?.Invoke();
        }

    }
}
using System;
using QuestSystem.LevelSystem.LevelUI;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem.LevelSystem
{

    public class LevelMap : MonoBehaviour
    {
        public UnityEvent OnLevelLoadEvent;
        [SerializeField] private Transform _startPosTrm;
        [SerializeField] private Collider2D _cameraConfinder;
        [SerializeField] private LevelCanvas _levelCanvas;

        public Vector2 StartPos => _startPosTrm.position;
        public Collider2D CameraConfiner => _cameraConfinder;

        private void Start()
        {
            if (_levelCanvas != null)
                _levelCanvas.Initialize();
        }

        // Call In Awake Timeline
        public void Initialize()
        {
            OnLevelLoadEvent?.Invoke();
        }

    }
}
using Core.Attribute;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StageController
{
    public abstract class StageSO : ScriptableObject
    {
        public event Action onValueChange;

        [ReadOnly]public ushort id;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        [HideInInspector] public StageSO prevStage;
        [HideInInspector] public StageSO nextStage;

        public bool isFirstStage;
        public string displayStageName;
        public Sprite stageIcon;
        public string sceneName;

        private void OnValidate()
        {
            onValueChange?.Invoke();
        }
    }

    public enum StageType
    {
        Ingame, Cafe, Office
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Office
{
    public abstract class StageSO : ScriptableObject
    {
        [HideInInspector] public ushort id;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        [HideInInspector] public StageSO prevMission;
        [HideInInspector] public List<StageSO> nextMissions;

        public string sceneName;
    }

    public enum StageType
    {

    }
}

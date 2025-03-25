using System.Collections.Generic;
using UnityEngine;

namespace Office
{
    [CreateAssetMenu(menuName = "SO/Office/Mission")]
    public class MissionSO : ScriptableObject
    {
        [HideInInspector] public ushort id;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        [HideInInspector] public MissionSO prevMission;
        public List<MissionSO> nextMissions;

        public MissionType missionType;
        public string missionName;
        public Sprite icon;
        [TextArea(5,10)]
        public string missionExplain;
        public int missionDefaultReward;
        [Range(1,5)]
        public int missionDifficulty;

        [Space(10)]
        public string sceneName;
    }

    public enum MissionType
    {
        Kill,
        Sweep,
        Collect,
        Hostage,
        Sub
    }
}

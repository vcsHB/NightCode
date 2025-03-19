using UnityEngine;

namespace Office
{
    [CreateAssetMenu(menuName = "SO/Basement/Mission")]
    public class MissionSO : ScriptableObject
    {
        public string missionName;
        public MissionType missionType;
        public Sprite icon;
        [TextArea(5,10)]
        public string missionExplain;
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

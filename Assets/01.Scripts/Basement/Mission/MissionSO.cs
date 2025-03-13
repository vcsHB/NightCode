using UnityEngine;

namespace Basement.Mission
{
    [CreateAssetMenu(menuName = "SO/Basement/Mission")]
    public class MissionSO : ScriptableObject
    {
        public string missionName;
        public MissionType missionType;
        public Sprite icon;
        [TextArea(5,10)]
        public string missionExplain;

        [Space(10)]
        public string sceneName;
    }

    public enum MissionType
    {
        Kill,
        Sweep,
        Collect,
        Hostage
    }
}

using UnityEngine;

namespace Basement.Mission
{
    [CreateAssetMenu(menuName = "SO/Basement/Mission")]
    public class MissionSO : ScriptableObject
    {
        public string missionName;
        [TextArea(5,10)]
        public string missionExplain;

        [Space(10)]
        public string sceneName;
    }
}

    using UnityEngine;
namespace Map
{

    [CreateAssetMenu(menuName = "SO/Map/Difficulty")]
    public class StageDifficultySO : ScriptableObject
    {
        public int level;
        public string difficultyName;
        public string description;
        public Color difficultyColor;
    }
}
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/Dialog/Emotion")]
    public class EmotionSO : ScriptableObject
    {
        public Sprite emotionIcon;
        public Vector2 offset;
    }
}

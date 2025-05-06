using UnityEngine;

namespace Dialog
{
    public class EmotionIconEvent : DialogEvent
    {
        public EmotionSO emotion;

        public override void PlayEvent(DialogPlayer dialogPlayer, Actor actor)
        {
            actor.personalEmotion.SetIcon(emotion.emotionIcon, emotion.offset);
        }
    }
}

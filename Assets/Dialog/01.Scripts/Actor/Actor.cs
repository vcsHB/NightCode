using Dialog;
using System;
using TMPro;
using UnityEngine;


namespace Dialog
{
    [Serializable]
    public class Actor
    {
        public string name;
        public Transform target;
        public SpriteRenderer spriteRenderer;
        public TalkBubble personalTalkBubble;
        public Emotion personalEmotion;

        public TextMeshProUGUI ContentText => personalTalkBubble.ContentTextMeshPro;

        public void OnCompleteNode()
        {
            personalTalkBubble.SetDisabled();
            personalEmotion.DisableEmotion();
        }

    }
}
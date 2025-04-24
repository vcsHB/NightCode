using CameraControllers;
using UnityEngine;

namespace Dialog
{
    public class FocusEvent : DialogEvent
    {
        public string focusCharacterName;
        public float easingDuration;

        public override void PlayEvent(DialogPlayer dialogPlayer, Actor actor)
        {
            //CameraManager.Instance.SetFollow();
        }
    }
}

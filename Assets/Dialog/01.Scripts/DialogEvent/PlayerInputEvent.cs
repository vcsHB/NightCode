using Cafe;
using System;
using UnityEngine;

namespace Dialog
{
    public class PlayerInputEvent : DialogEvent
    {
        public CafeInput cafeInput;
        public bool isEnable;

        public override void PlayEvent()
        {
            isCompleteEvent = true;
            if (isEnable) cafeInput.EnableInput();
            else cafeInput.DisableInput();
        }
    }
}

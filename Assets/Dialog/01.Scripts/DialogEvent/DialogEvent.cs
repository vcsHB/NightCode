using System;
using System.Threading;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public abstract class DialogEvent
    {
        public bool stopUntilCompleteEvent;
        [HideInInspector] public bool isCompleteEvent = true;

        public virtual void PlayEvent(DialogPlayer dialogPlayer, Actor actor)
        {
            isCompleteEvent = !stopUntilCompleteEvent;
        }
    }
}

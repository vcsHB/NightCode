using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public abstract class StopReadingAnimation : TagAnimation
    {
        protected DialogPlayer _player;

        public virtual void Init(DialogPlayer player)
        {
            _player = player;
        }
    }
}

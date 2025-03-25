using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class PlayTagAnimation : SpriteAnimation
    {
        private int _animBoolHash;
        private bool _animEnd = false;

        public PlayTagAnimation()
        {
            _timing = AnimTiming.Start;
            tagType = TagEnum.Play;
            _checkEndPos = false;
        }

        public override void Play()
        {
            if (_animEnd) return;

            var charInfo = _txtInfo.characterInfo[animStartPos - 1];
            if (!charInfo.isVisible) return;

            _animator.SetBool(_animBoolHash, true);
        }

        public override void Complete()
        {
            _animator.SetBool(_animBoolHash, false);
            _animEnd = true;
        }

        public override bool SetParameter()
        {
            _animBoolHash = Animator.StringToHash(Param);
            return true;
        }

        public override void Init()
        {
            base.Init();
            _animEnd = false;
        }
    }
}

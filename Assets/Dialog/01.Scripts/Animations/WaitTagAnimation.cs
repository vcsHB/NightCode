using System.Collections;
using UnityEngine;

namespace Dialog
{
    public class WaitTagAnimation : StopReadingAnimation
    {
        private float _delay;
        private float _animStartTime;

        private bool animStartFlag = true;

        public WaitTagAnimation()
        {
            _timing = AnimTiming.OnTextOut;
            tagType = TagEnum.Wait;
            _stopReadingDuringAnimation = true;
            _checkEndPos = false;
        }

        public override void Play()
        {
            var charInfo = _txtInfo.characterInfo[animStartPos - 1];

            if(charInfo.isVisible && animStartFlag)
            {
                animStartFlag = false;
                _animStartTime = Time.time;
                _player.stopReading = true;
            }

            if (_animStartTime + _delay <= Time.time)
            {
                _player.stopReading = false;
            }
        }

        public override void Complete()
        {

        }

        public override bool SetParameter()
        {
            if (float.TryParse(Param, out _delay) == false)
            {
                Debug.LogError($"{tagType.ToString()} ({Param}) : Parameter is wrong");

                return false;
            }
            return true;
        }

        private IEnumerator Delay()
        {
            int delay = int.Parse(Param);
            yield return new WaitForSeconds(delay);

        }
    }
}

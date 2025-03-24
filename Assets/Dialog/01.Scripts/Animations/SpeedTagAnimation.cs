using UnityEngine;

namespace Dialog
{
    public class SpeedTagAnimation : StopReadingAnimation
    {
        private float _originSpeed;
        private float _speed;

        public SpeedTagAnimation()
        {
            _checkEndPos = true;
        }

        public override void OnStartTag()
        {
            Debug.Log(_player.TextOutDelay);
            _originSpeed = _player.TextOutDelay;
            _player.SetTextOutDelay(_speed);
        }

        public override void OnEndTag()
        {
            Debug.Log(_originSpeed);
            _player.SetTextOutDelay(_originSpeed);
        }

        public override bool SetParameter()
        {
            if (float.TryParse(Param, out _speed) == false)
            {
                Debug.LogError($"{tagType.ToString()} ({Param}) : Parameter is wrong");
                return false;
            }
            return true;
        }

        public override void Complete() { }
    }
}

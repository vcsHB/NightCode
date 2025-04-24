using TMPro;
using UnityEngine;

namespace Dialog
{
    public abstract class TagAnimation
    {
        protected TMP_TextInfo _txtInfo;

        protected AnimTiming _timing;
        protected string _param;        //�Ķ����
        protected bool _checkEndPos;    //���� Ȯ���ϴ��� </�� ã�ƾ��ϴ��� Ȯ���ϴ°���
        protected bool _endAnimating;   //Ÿ�̹��� Start�� End�� �� ���� �ٲ㼭 Ȯ�Τ�
        protected bool _stopReadingDuringAnimation = false;
        protected bool _animationComplete = false;
        protected bool _isTagStart = false;
        protected bool _isTagEnd = false;

        public TagEnum tagType;
        public int animStartPos;
        public int animLength;

        public AnimTiming Timing => _timing;
        public string Param => _param;
        public bool EndAnimating => _endAnimating;
        public bool CheckEndPos => _checkEndPos;
        public bool StopReadingDuringAnimation => _stopReadingDuringAnimation;

        public void SetParameter(string param) => _param = param;

        public virtual void OnStartTag()
        {

        }

        public virtual void OnEndTag()
        {

        }

        public virtual void Play()
        {
            bool start = CheckTextEnable(animStartPos - 1);
            bool end = CheckTextEnable((animStartPos + animLength));

            if (start && !_isTagStart)
            {
                OnStartTag();
                _isTagStart = true;
            }

            if (end && !_isTagEnd)
            {
                OnEndTag();
                _isTagEnd = true;
            }
        }

        private bool CheckTextEnable(int index)
        {
            while (_txtInfo.textComponent.text[index] == ' ')
            {
                index--;
                if (index <= 0) break;
            }

            //Debug.Log(_txtInfo.textComponent.text.Length);
            //Debug.Log(index);
            //Debug.Log("_txtInfo.characterInfo.Len : " + _txtInfo.characterInfo.Length);
            return _txtInfo.characterInfo[index].isVisible;
        }

        public abstract void Complete();
        public abstract bool SetParameter();

        public virtual void Init()
        {
            _endAnimating = false;
            _animationComplete = false;
            _isTagStart = false;
            _isTagEnd = false;
        }

        public virtual void SetTextInfo(TMP_TextInfo txtInfo)
        {
            _endAnimating = false;
            _txtInfo = txtInfo;
        }
    }

    public enum AnimTiming
    {
        Start,
        Update,
        OnTextOut,
        End
    }
}

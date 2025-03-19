using TMPro;

namespace Dialog
{
    public abstract class TagAnimation
    {
        protected TMP_TextInfo _txtInfo;

        protected AnimTiming _timing;
        protected string _param;        //파라미터
        protected bool _checkEndPos;    //끝을 확인하는지 </를 찾아야하는지 확인하는거임
        protected bool _endAnimating;   //타이밍이 Start나 End일 때 저걸 바꿔서 확인ㄱ
        protected bool _stopReadingDuringAnimation = false;
        protected bool _animationComplete = false;

        public TagEnum tagType;
        public int animStartPos;
        public int animLength;

        public AnimTiming Timing => _timing;
        public string Param => _param;
        public bool EndAnimating => _endAnimating;
        public bool CheckEndPos => _checkEndPos;
        public bool StopReadingDuringAnimation => _stopReadingDuringAnimation;

        public void SetParameter(string param) => _param = param;

        public abstract void Play();
        public abstract void Complete();
        public abstract bool SetParameter();

        public virtual void Init()
        {
            _endAnimating = false;
            _animationComplete = false;
        }

        public virtual void SetTextInfo(TMP_TextInfo txtInfo)
        {
            _endAnimating = false;
            _txtInfo = txtInfo;
        }
    }

    public enum AnimTiming
    {
        //Start랑 Update는 둘다 LateUpdate에서 실행해줘도 됨
        //근데 End는 끝낼 때 while문안에 잡아두고 해야함
        //OnTextOut은 텍스트 출력할 때 While문에 잡아두고
        Start,
        Update,
        OnTextOut,
        End
    }
}

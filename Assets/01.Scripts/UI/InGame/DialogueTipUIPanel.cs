using DG.Tweening;
using UnityEngine;

namespace UI.InGame.SystemUI
{
    public class DialogueTipUIPanel : UIPanel
    {
        [SerializeField] private RectTransform _textTrm;
        [SerializeField] private float _duration;
        [SerializeField] private float _activePos;
        [SerializeField] private float _defaultPos;

        public override void Open()
        {
            base.Open();
            _textTrm.DOAnchorPosY(_activePos, _activeDuration);
        }

        public override void Close()
        {
            base.Close();
            _textTrm.DOAnchorPosY(_defaultPos, _activeDuration);

        }
    }

}
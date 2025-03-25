using DG.Tweening;
using UnityEngine;

namespace Office
{
    public class SkillTreePanel : OfficeUIParent
    {
        [Space]
        public SkillTree[] skillTrees;
        [SerializeField] private float _easingDuration;

        private Tween _openCloseTween;

        public void InitSkillTree(CharacterEnum characterType)
        {
            for (int i = 0; i < skillTrees.Length; i++)
            {
                if (i == (int)characterType)
                {
                    skillTrees[i].Open();
                }
                else
                {
                    skillTrees[i].Close();
                }
            }
        }

        public override void OpenAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(0, _easingDuration)
                .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(-1080, _easingDuration)
                .OnComplete(OnCompleteClose);
        }
    }
}

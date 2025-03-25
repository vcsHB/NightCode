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
        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        private void Awake()
        {
            RectTrm.anchoredPosition = new Vector2(0, -screenPosition.y);
        }

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

            _openCloseTween = RectTrm.DOAnchorPosY(-screenPosition.y, _easingDuration)
                .OnComplete(OnCompleteClose);
        }
    }
}

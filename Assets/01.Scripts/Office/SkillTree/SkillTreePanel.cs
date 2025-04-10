using DG.Tweening;
using UnityEngine;
using Office.CharacterSkillTree;
using System.Collections.Generic;
using System.IO;
using System;
using StatSystem;

namespace Office
{
    public class SkillTreePanel : OfficeUIParent
    {
        [Space]
        public SkillTree[] skillTrees;
        [SerializeField] private CharacterStatIndicator _statIndicator;
        [SerializeField] private float _easingDuration;

        private Tween _openCloseTween;
        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        private void Awake()
        {
            RectTrm.anchoredPosition = new Vector2(0, -screenPosition.y);
        }

        #region UI

        public void UpdateSkillTree(CharacterEnum characterType)
        {
            _statIndicator.SetCharacter(skillTrees[(int)characterType]);
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

        public void InitSkillTree(CharacterEnum characterType)
        {
            _statIndicator.SetCharacter(skillTrees[(int)characterType]);
            for (int i = 0; i < skillTrees.Length; i++)
            {
                skillTrees[i].Init();
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
        #endregion
    }
}


using DG.Tweening;
using System;
using UnityEngine;

public class SkillTreePanel : MonoBehaviour, IUIPanel
{
    public SkillTree[] skillTrees;
    private float _easingTime = 0.2f;

    private RectTransform RectTrm => transform as RectTransform;

    public void OpenSkillTree(CharacterType characterType)
    {
        for (int i = 0; i < skillTrees.Length; i++)
        {
            if (i == (int)characterType)
            {
                skillTrees[(int)characterType].Open(Vector2.zero);
            }
            else
            {
                skillTrees[(int)characterType].Close();
            }
        }
    }


    public void Open(Vector2 position)
    {
        RectTrm.DOAnchorPosY(0f, _easingTime);
        OpenSkillTree(0);
    }

    public void Close()
    {
        RectTrm.DOAnchorPosY(-1920f, _easingTime);
    }
}

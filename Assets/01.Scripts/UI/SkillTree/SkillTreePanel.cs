using Basement.Training;
using DG.Tweening;
using UI;
using UnityEngine;

public class SkillTreePanel : MonoBehaviour, IWindowPanel
{
    public SkillTree[] skillTrees;
    private float _easingTime = 0.2f;

    [SerializeField] private CharacterStatPointIndicator _characterStatPointIndicator;

    private RectTransform RectTrm => transform as RectTransform;

    public void OpenSkillTree(CharacterEnum characterType)
    {
        _characterStatPointIndicator.SetCharacter(characterType);

        for (int i = 0; i < skillTrees.Length; i++)
        {
            if (i == (int)characterType)
            {
                skillTrees[i].Open(Vector2.zero);
            }
            else
            {
                skillTrees[i].Close();
            }
        }
    }


    public void Open()
    {
        RectTrm.DOAnchorPosY(0f, _easingTime);
        OpenSkillTree(0);
    }

    public void Close()
    {
        RectTrm.DOAnchorPosY(-1920f, _easingTime);
    }
}

using Basement;
using Basement.Training;
using DG.Tweening;
using UI;
using UnityEngine;

public class SkillTreePanel : BasementCommonUI
{
    [Space]
    public SkillTree[] skillTrees;
    public OfficeUI _office;
    [SerializeField] private CharacterStatPointIndicator _characterStatPointIndicator;


    public void InitSkillTree(CharacterEnum characterType)
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

    public override void Open()
    {
        InitSkillTree(0);
        base.Open();
    }
    protected override void CloseAnimation()
    {
        base.CloseAnimation();
        _office.characterSelectPanel.ReturnToSelectPanel();
    }
    public void Init(OfficeUI office)
    {
        _office = office;
    }
}

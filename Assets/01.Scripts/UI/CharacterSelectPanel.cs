using System;
using UnityEngine;

public class CharacterSelectPanel : MonoBehaviour
{
    public delegate void CharacterSelectAction(CharacterType characterType);   

    public event CharacterSelectAction OnSelectCharacter;
    public CharacterSelectButton[] characterSelectBtn;

    private RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            CharacterType character = (CharacterType)i;
            characterSelectBtn[i].character = character;
            characterSelectBtn[i].OnClickEvent += OnClickBtn;
        }
    }

    public void EnableSelectPanel(CharacterSelectAction onCharacterSelect , Vector2 position)
    {
        gameObject.SetActive(true);
        RectTrm.anchoredPosition = position;
        OnSelectCharacter = onCharacterSelect;
    }

    public void OnClickBtn(CharacterType characterType)
    {
        OnSelectCharacter?.Invoke(characterType);
        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectPanel : MonoBehaviour, IUIPanel
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

    public void SetSelectAction(CharacterSelectAction onCharacterSelect)
    {
        OnSelectCharacter = onCharacterSelect;
    }

    public void EnableSelectPanel(CharacterSelectAction onCharacterSelect , Vector2 position)
    {
    }

    public void OnClickBtn(CharacterType characterType)
    {
        OnSelectCharacter?.Invoke(characterType); 
        Close();
    }

    public void Open(Vector2 position)
    {
        gameObject.SetActive(true);
        RectTrm.anchoredPosition = position;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

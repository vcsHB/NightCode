using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectButton : MonoBehaviour, IPointerClickHandler
{
    //public event Action<CharacterType> OnClickEvent;
    //public CharacterType character;

    public void OnPointerClick(PointerEventData eventData)
    {
        //여기서 뭐 다른 것들 추가로 실행 시켜 줄 수 있으면 실행 시켜도 되고
        //OnClickEvent?.Invoke(character);
    }
}

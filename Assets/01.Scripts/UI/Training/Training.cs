using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Training : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TrainingSO training;
    public bool completeTraining = false;

    [SerializeField] private UIPopupText _popupText;

    public RectTransform RectTrm => transform as RectTransform;

    public void DoTraining(CharacterType character)
    {
        if (completeTraining && training == null) return;

        completeTraining = true;

        StatType statType = training.statType;
        float percent = Random.Range(0.00f, 100.00f);

        int incValue = percent <= training.greatSuccesChance ? training.greatSuccessValue :
            percent <= training.successChance ? training.successValue : 0;

        AgentStatManager.Instance.AddStatPoint(character, statType, incValue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}

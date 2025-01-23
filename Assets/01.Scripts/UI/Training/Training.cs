using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Training : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TrainingSO training;
    public bool completeTraining = false;

    [SerializeField] private UIPopupText _popupText;
    [SerializeField] private GameObject _trainedIndicator;
    private CharacterSelectPanel _selectPanel;
    private Transform _canvasTrm;

    public RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {
        _canvasTrm = transform.GetComponentInParent<Canvas>().transform;
        UnSetCompleteTraining();
    }

    public void DoTraining(CharacterType character)
    {
        if (completeTraining || training == null) return;

        completeTraining = true;

        StatType statType = training.statType;
        float percent = Random.Range(0.00f, 100.00f);

        TrainingResult trainingResult = percent <= training.greatSuccesChance ? TrainingResult.GreatSuccess :
            percent <= training.successChance ? TrainingResult.Success : TrainingResult.Fail;

        int incValue = training.increaseValue[trainingResult];
        Color textColor = training.textColor[trainingResult];


        //여기서 나중에 바로 팝업텍스트 나오게 하는게 아니라 캐릭터 훈련 애니메이션이 나오고, 애니메이션이 끝났을 때 
        UIPopupText popupText = Instantiate(_popupText, _canvasTrm);
        popupText.SetText($"{statType.ToString()} +{incValue}", textColor, 50, 0.5f, 1, RectTrm.localPosition);

        AgentStatManager.Instance.AddStatPoint(character, statType, "TRAINING", incValue);
        SetCompleteTraining();
    }

    public void SetCompleteTraining()
    {
        _trainedIndicator.SetActive(true);
    }

    public void UnSetCompleteTraining()
    {
        _trainedIndicator.SetActive(false);
    }

    #region InputEventRegion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (completeTraining || training == null) return;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void Init(CharacterSelectPanel selectPanel)
    {
        _selectPanel = selectPanel;
    }

    #endregion
}

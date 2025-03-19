using Basement.Mission;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _missionTypeText;
    [SerializeField] private TextMeshProUGUI _missionNameText;
    [SerializeField] private TextMeshProUGUI _explainText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _selectMissionButton;

    public RectTransform RectTrm => transform as RectTransform;
    public RectTransform childRect => transform.GetChild(0) as RectTransform;
    public MissionSO Mission => _mission;

    private MissionSelectPanel _selectPanel;
    private bool _isSelected = false;
    private MissionSO _mission;
    private Tween _tween;


    private void OnDisable()
    {
        _selectMissionButton.onClick.RemoveListener(OnClickButton);
    }

    public void Init(MissionSO mission)
    {
        _mission = mission;
        _missionTypeText.SetText(_mission.missionType.ToString());
        _missionNameText.SetText(_mission.missionName);
        _explainText.SetText(_mission.missionExplain);
        _icon.sprite = _mission.icon;
        _selectPanel = GetComponentInParent<MissionSelectPanel>();
    }

    public void UnSelectButton()
    {
        _isSelected = false;
        _selectMissionButton.onClick.RemoveListener(OnClickButton);
    }

    public void OnClickButton()
    {
        //이것도 뭐 씬 로드 그런걸로 바꾸고 그런느낌?
        SceneManager.LoadScene(_mission.sceneName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelected = true;

        if (_tween != null && _tween.active) _tween.Kill();
        _tween = childRect.DOAnchorPosY(0, 0.2f)
            .OnComplete(() => _selectMissionButton.onClick.AddListener(OnClickButton));

        _selectPanel.SelectPanel(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected) return;
        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = childRect.DOAnchorPosY(0, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected) return;
        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = childRect.DOAnchorPosY(150, 0.2f);
    }
}

using QuestSystem;
using QuestSystem.QuestTarget;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.QuestSyetem
{

    public class ProgressProfile : MonoBehaviour
    {
        [SerializeField] private Image _edgeImage;
        [SerializeField] private Image _targetIconImage;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _completeColor;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
        public void Initialize(TargetInfoSO targetData)
        { // 타겟의 프로필 정보를 넘겨야됨
            _targetIconImage.sprite = targetData.targetIcon;

        }

        public void SetComplete(bool value)
        {
            _edgeImage.color = value ? _completeColor : _defaultColor;
            _targetIconImage.color = value ? Color.red : Color.white;
        }
    }
}
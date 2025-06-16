using Core;
using DG.Tweening;
using Map;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.NodeViewScene.StageSelectionUIs
{
    public class StageSelectionPanel : MonoBehaviour, IWindowPanel
    {
        public event Action OnSelectMap;

        [Header("UI Setting")]
        [SerializeField] private float _enablePosition = -10f;
        [SerializeField] private float _disablePosition = 500f;
        [SerializeField] private float _tweenDuration = 0.4f;

        [Header("Info View Setting")]
        [SerializeField] private MapController _mapController;
        [SerializeField] private SelectButton _selectButton;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private DifficultyDisplayer _difficultyDisplayer;
        [SerializeField] private ColoringImage[] _colorImages;

        private MapNode _selectedNode;
        private RectTransform _rectTrm;
        private Tween _currentTween;

        private void Awake()
        {
            _rectTrm = GetComponent<RectTransform>();
            if (_mapController != null)
            {
                _mapController.OnClickNodeEvent += HandleSelectMapNode;
            }
            _selectButton.OnClickEvent += SelectMap;
        }

        private void SelectMap()
        {
            //_mapController.SetCompleteNode(_selectedNode.Position);  //이건 나중에 씬 로딩할 때 받아서 해야함
            _mapController.SaveEnterStage(_selectedNode);
            SceneManager.LoadScene(SceneName.InGameScene);
            //씬 로딩
        }

        public void Open()
        {
            _currentTween?.Kill();
            _currentTween = _rectTrm.DOAnchorPosX(_enablePosition, _tweenDuration);
        }

        public void Close()
        {
            _currentTween?.Kill();
            _currentTween = _rectTrm.DOAnchorPosX(_disablePosition, _tweenDuration);
        }

        private void HandleSelectMapNode(MapNode data)
        {
            
            if (data.characterIcons.Count == 0 || data.IsComplete || 
                (data.characterIcons[0].IsMoved == false && data.characterIcons[0].IsCompleteCurerntLevel)) return;

            //if(data.)
            _selectedNode = data;
            _currentTween?.Kill();

            _currentTween = DOTween.Sequence()
                .Append(_rectTrm.DOAnchorPosX(_disablePosition, _tweenDuration))
                .AppendCallback(() => SetDataView(data.NodeInfo))
                .Append(_rectTrm.DOAnchorPosX(_enablePosition, _tweenDuration));
        }

        private void SetDataView(MapNodeSO data)
        {
            if (data == null) return;

            _titleText.text = data.displayName;
            _descriptionText.text = data.explain;
            _difficultyDisplayer.SetDifficulty(data.difficulty);
            SetImageColors(data.color);
        }

        private void SetImageColors(Color color)
        {
            for (int i = 0; i < _colorImages.Length; i++)
            {
                _colorImages[i].SetColor(color);
            }
        }
    }
}

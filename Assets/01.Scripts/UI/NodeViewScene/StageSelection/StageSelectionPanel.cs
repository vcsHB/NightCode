using System;
using DG.Tweening;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.NodeViewScene.StageSelectionUIs
{
    public class StageSelectionPanel : MonoBehaviour, IWindowPanel
    {
        [Header("UI Setting")]
        [SerializeField] private float _enablePosition = -10f;
        [SerializeField] private float _disablePosition = 500f;
        [SerializeField] private float _tweenDuration = 0.4f;

        [Header("Info View Setting")]
        [SerializeField] private MapGraph _mapGraph;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private DifficultyDisplayer _difficultyDisplayer;
        [SerializeField] private ColoringImage[] _colorImages;

        private RectTransform _rectTrm;
        private Tween _currentTween;

        private void Awake()
        {
            _rectTrm = GetComponent<RectTransform>();
            if (_mapGraph != null)
            {
                _mapGraph.OnClickNodeEvent += HandleSelectMapNode;
            }
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

        private void HandleSelectMapNode(MapNodeSO data)
        {
            _currentTween?.Kill();

            _currentTween = DOTween.Sequence()
                .Append(_rectTrm.DOAnchorPosX(_disablePosition, _tweenDuration))
                .AppendCallback(() => SetDataView(data))
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

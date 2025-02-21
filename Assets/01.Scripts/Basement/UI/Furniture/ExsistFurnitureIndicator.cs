using Basement;
using DG.Tweening;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Basement
{
    public class ExsistFurnitureIndicator : MonoBehaviour, IWindowPanel
    {
        public FurnitureSetSO furnitureSet;
        [SerializeField] private Transform _frameTrm;
        [SerializeField] private FurnitureIcon _iconPrefab;
        [SerializeField] private FurnitureExplainUI _explain;

        private List<FurnitureIcon> _exsistIcon;
        private BasementRoom _currentRoom;
        private Tween _tween;

        private RectTransform _rectTrm => transform as RectTransform;

        private void Awake()
        {
            _exsistIcon = new List<FurnitureIcon>();
        }

        public void SetBasementRoom(BasementRoom room)
        {
            _currentRoom = room;

            _exsistIcon.ForEach(icon => Destroy(icon.gameObject));
            _exsistIcon.Clear();

            room.furnitureList.ForEach(furniture =>
            {
                FurnitureSO furnitureSO = furniture.furnitureSO;

                FurnitureIcon icon = Instantiate(_iconPrefab, _frameTrm);
                icon.SetFurniture(furnitureSO);
                icon.OnClick += _explain.SetFurniture;
                _exsistIcon.Add(icon);
            });
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(0f, 0.3f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(-97f, 0.3f);
        }
    }
}

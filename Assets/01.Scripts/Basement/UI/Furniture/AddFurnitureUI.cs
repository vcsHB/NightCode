using DG.Tweening;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Basement
{
    public class AddFurnitureUI : MonoBehaviour, IWindowPanel
    {
        public List<FurnitureSO> furnitureWhatIHave;
        public Transform contentTrm;
        public FurnitureIcon iconPf;

        private ExsistFurnitureIndicator _indicator;
        private List<FurnitureIcon> _iconList = new List<FurnitureIcon>();
        private BasementRoom _room;
        private Tween _tween;
        private RectTransform _rectTrm => transform as RectTransform;

        public void Init(BasementRoom room, ExsistFurnitureIndicator indicator)
        {
            _room = room;
            _indicator = indicator;
        }

        public void Close()
        {
            if (_tween != null && _tween.active) _tween.Kill();
            _tween = _rectTrm.DOAnchorPosY(-120, 0.3f);
        }

        public void Open()
        {
            _iconList.ForEach(icon =>
            {
                if (icon != null)
                    Destroy(icon.gameObject);
            });
            _iconList.Clear();

            if (_tween != null && _tween.active) _tween.Kill();
            _tween = _rectTrm.DOAnchorPosY(0, 0.3f);

            furnitureWhatIHave.ForEach(furniture =>
            {
                FurnitureIcon icon = Instantiate(iconPf, contentTrm);
                icon.SetFurniture(furniture);
                icon.OnClick += furniture =>
                {
                    _room.AddFurniture(furniture, Vector2.zero);
                    furnitureWhatIHave.Remove(furniture);
                    _indicator.SetBasementRoom(_room);
                    Destroy(icon.gameObject);
                };

                _iconList.Add(icon);
            });
        }
    }
}

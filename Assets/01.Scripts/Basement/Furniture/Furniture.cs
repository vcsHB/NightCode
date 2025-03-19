using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Basement
{
    public class Furniture : IngameInteractiveObject
    {
        public Action InteractAction;
        public FurnitureSO furnitureSO;
        [SerializeField] private bool _stickToGround;

        private BasementRoom _room;
        private BoxCollider2D _roomCollider;
        private Vector2 _roomSize;
        private Vector2 _furnitureSize;
        private Vector2 _offset;

        public void Init(BasementRoom basementRoom)
        {
            _room = basementRoom;
            _roomCollider = _room.GetComponent<BoxCollider2D>();
            _roomSize = _roomCollider.size;
            _furnitureSize = GetComponent<BoxCollider2D>().size;
        }

        public void SetPosition(Vector2 target)
        {
            Vector2 maxDist = (_roomSize / 2 - _furnitureSize / 2);
            Vector2 minPosition = (Vector2)_room.transform.position - maxDist;
            Vector2 maxPosition = (Vector2)_room.transform.position + maxDist;
            Vector2 position = target + _offset;

            transform.position = new Vector2(
                Mathf.Clamp(position.x, minPosition.x, maxPosition.x),
                _stickToGround ? minPosition.y : Mathf.Clamp(position.y, minPosition.y, maxPosition.y));
        }

        protected override void OnMouseLeftButtonDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (_room.BasementController.GetCurrentBasementMode() == BasementMode.Basement) return;

            base.OnMouseLeftButtonDown();

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            _offset = (Vector2)transform.position - mousePosition;
        }

        protected override void OnMouseLeftButtonUp()
        {
            base.OnMouseLeftButtonUp();
            
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (_room.BasementController.GetCurrentBasementMode() == BasementMode.Build) return;

            InteractAction?.Invoke();
        }

        protected override void OnDrag(Vector2 mousePosition)
        {
            if (_room.BasementController.GetCurrentBasementMode() == BasementMode.Basement) return;

            Vector2 mosuePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            SetPosition(mosuePosition);
            base.OnDrag(mousePosition);
        }
    }
}


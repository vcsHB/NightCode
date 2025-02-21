using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using NUnit.Framework.Constraints;
using Basement.CameraController;

namespace Basement
{
    public class Furniture : MonoBehaviour
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

        private void OnMouseDown()
        {
            if (_room.IsFurnitureSettingMode == false) return;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            _offset = (Vector2)transform.position - mousePosition;
        }

        private void OnMouseDrag()
        {
            if (_room.IsFurnitureSettingMode == false) return;

            Vector2 mosuePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            SetPosition(mosuePosition);
        }
    }
}


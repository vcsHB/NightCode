using Basement.CameraController;
using Basement.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Basement
{
    public class BasementController : MonoBehaviour
    {
        public Action<BasementMode> OnChangeBasmentMode;

        [SerializeField] private BasementInput _input;
        [SerializeField] private GameObject _buildModeObj;
        [SerializeField] private Office _office;
        [SerializeField] private float _dragSkipDist = 2;
        [SerializeField] private float _dragMaxDist = 3;

        private BasementMode _currentMode = BasementMode.Basement;
        private List<BasementBuildUI> _buildUISet;
        private BasementTimerUI _timer;

        private BasementRoom[,] _basementRooms = new BasementRoom[4, 3];
        private BasementRoom _currentRoom;
        private int _currentFloor, _currentRoomNumber;
        private bool _isMousePressed = false;
        private Vector2 _dragValue;

        private void Awake()
        {
            _input.onPressLeftclick += MouseEvent;
            _buildUISet = _buildModeObj.GetComponentsInChildren<BasementBuildUI>().ToList();
            _timer = UIManager.Instance.timer;

            Office office = FindAnyObjectByType<Office>();
            Cafe cafe = FindAnyObjectByType<Cafe>();
            _basementRooms[0, 0] = office;
            _basementRooms[0, 1] = _basementRooms[0, 2] = cafe;

            office.Init(this);
            cafe.Init(this);
        }

        private void OnDisable()
        {
            _input.onPressLeftclick -= MouseEvent;
        }

        public void Update()
        {
            //TODO: Change to newinput

            if (_isMousePressed)
            {
                float cameraSize = BasementCameraManager.Instance.CameraSize;
                _dragValue = (_input.MousePosition - _input.MouseClickpoint) / -500 * cameraSize;
                _dragValue.y = 0;
                _dragValue.x = Mathf.Clamp(_dragValue.x, -_dragMaxDist, _dragMaxDist);

                BasementCameraManager.Instance.OffsetCamera(_dragValue);
            }
        }

        public void OnFocusRoom(BasementRoom room)
        {
            _currentRoom = room;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_basementRooms[i, j] == room)
                    {
                        _currentFloor = i;
                        _currentRoomNumber = j;

                        bool canGoLeft = (_currentRoomNumber > 0 && _basementRooms[_currentFloor, _currentRoomNumber - 1] != null);
                        bool canGoRight = (_currentRoomNumber < 2 && _basementRooms[_currentFloor, _currentRoomNumber + 1] != null);
                        if (_currentFloor == 0 && _currentRoomNumber == 1) canGoRight = false;

                        UIManager.Instance.basementUI.OnChangeRoom(canGoLeft, canGoRight);
                        return;
                    }
                }
            }
        }

        public void MouseEvent(bool isPress)
        {
            _isMousePressed = isPress;

            if (isPress == false)
            {
                bool isChanged = false;

                if (_currentRoomNumber >= 0 && _currentRoomNumber < 3)
                {
                    if (_dragValue.x > _dragSkipDist && _currentRoomNumber < 2
                        && _basementRooms[_currentFloor, _currentRoomNumber + 1] != null)
                    {
                        isChanged = true;
                        MoveRight();
                    }
                    else if (_dragValue.x < -_dragSkipDist && _currentRoomNumber > 0
                        && _basementRooms[_currentFloor, _currentRoomNumber - 1] != null)
                    {
                        isChanged = true;
                        MoveLeft();
                    }
                }

                if (isChanged == false)
                {
                    
                    BasementCameraManager.Instance.ResetCameraOffset();
                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    _isMousePressed = false;
                    return;
                }
                BasementCameraManager.Instance.StartDrag();
            }
        }

        public void MoveLeft()
        {
            _basementRooms[_currentFloor, _currentRoomNumber--].CloseUI();
            _basementRooms[_currentFloor, _currentRoomNumber].FocusCamera();
            _basementRooms[_currentFloor, _currentRoomNumber].OpenRoomUI();
        }

        public void MoveRight()
        {
            _basementRooms[_currentFloor, _currentRoomNumber++].CloseUI();
            _basementRooms[_currentFloor, _currentRoomNumber].FocusCamera();
            _basementRooms[_currentFloor, _currentRoomNumber].OpenRoomUI();
        }

        public void SetRoom(BasementRoom room, int floor, int roomNumber)
            => _basementRooms[floor, roomNumber] = room;

        public void ChangeBuildMode(bool isBuildMode)
        {
            if (isBuildMode) _currentMode = BasementMode.Build;
            else _currentMode = BasementMode.Basement;

            OnChangeBasmentMode?.Invoke(_currentMode);
            _buildUISet.ForEach(buildUI =>
            {
                if (isBuildMode)
                    buildUI.Open();
                else
                    buildUI.Close();
            });
        }

        public BasementMode GetCurrentBasementMode() => _currentMode;

        public void CompleteScadule()
        {
            _timer.Close();
            _office.FocusRoom();
        }
    }

    public enum BasementMode
    {
        Basement,
        Build
    }
}
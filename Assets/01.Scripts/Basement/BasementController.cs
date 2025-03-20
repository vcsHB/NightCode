using Basement.CameraController;
using Basement.Training;
using Office;
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
        public bool IsFocusRoom { get; private set; }

        [SerializeField] private LayerMask _whatIsBasementObject;
        [SerializeField] private OfficeInput _input;
        [SerializeField] private GameObject _buildModeObj;
        [SerializeField] private Office _office;
        [SerializeField] private float _dragSkipDist = 2;
        [SerializeField] private float _dragMaxDist = 3;
        [SerializeField] private float _cameraMoveSpeed = 10;

        private BasementMode _currentMode = BasementMode.Basement;
        private List<BasementBuildUI> _buildUISet;
        private BasementTimerUI _timer;
        private bool _isWideView = false;

        private Collider2D mouseTarget;
        private Vector2 _mouseOffsetTemp;
        private Vector2 _prevMouseOffset;
        private Vector2 _clickPos;
        private BasementRoom _currentRoom;
        private BasementRoom[,] _basementRooms = new BasementRoom[4, 3];
        private int _currentFloor, _currentRoomNumber;
        private bool _isMousePressed = false;
        private Vector2 _dragValue;
        private readonly float _screenRatio = (float)Screen.height / (float)Screen.width;
        private readonly float _cameraMultiply = 500;

        private void Awake()
        {
            _input.onPressLeftclick += MouseEvent;
            _buildUISet = _buildModeObj.GetComponentsInChildren<BasementBuildUI>().ToList();
            _timer = UIManager.Instance.timer;

            Office office = FindAnyObjectByType<Office>();
            //Cafe cafe = FindAnyObjectByType<Cafe>();
            _basementRooms[0, 0] = office;
            //_basementRooms[0, 1] = _basementRooms[0, 2] = cafe;

            office.Init(this);
            //cafe.Init(this);
        }

        private void OnDisable()
        {
            _input.onPressLeftclick -= MouseEvent;
        }

        public void Update()
        {
            if (_currentRoom != null && _currentRoom.IsUIOpend) _isMousePressed = false;
            if (_isMousePressed) MouseDragEvent();
        }

        #region MouseEvent

        public void MouseEvent(bool isPress)
        {
            if (EventSystem.current.IsPointerOverGameObject() && isPress) return;
            if (_currentRoom != null && _currentRoom.IsUIOpend) return;

            _isMousePressed = isPress;
            if (CheckIngameObjectInteract(isPress)) return;

            if (isPress)
            {
                _clickPos = _input.MousePosition;
                if (_isWideView) _prevMouseOffset = _mouseOffsetTemp;
            }
            else
            {
                MouseUpEvent();
            }

        }

        private void MouseUpEvent()
        {
            if (_isWideView) return;
            bool isChanged = false;

            int x = 0;
            int y = 0;

            if (_dragValue.x > _dragSkipDist && _currentRoomNumber < 2)
            {
                isChanged = true;
                x = 1;
            }
            else if (_dragValue.x < -_dragSkipDist && _currentRoomNumber > 0)
            {
                isChanged = true;
                x = -1;
            }

            if (_dragValue.y > _dragSkipDist * _screenRatio && _currentFloor > 0)
            {
                isChanged = true;
                y = -1;
            }
            else if (_dragValue.y < -_dragSkipDist * _screenRatio && _currentFloor < 3)
            {
                isChanged = true;
                y = 1;
            }

            if (_basementRooms[_currentFloor + y, _currentRoomNumber + x] == null) isChanged = false;

            if (isChanged == false)
            {
                BasementCameraManager.Instance.ResetCameraOffset();
                _dragValue = Vector2.zero;
            }
            else
            {
                CameraMove(x, y);
            }
        }

        private void MouseDragEvent()
        {
            float cameraSize = BasementCameraManager.Instance.CameraSize;
            _dragValue = ((_input.MousePosition - _clickPos) / -_cameraMultiply * cameraSize);
            
            if (!_isWideView)
            {
                _dragValue.x = Mathf.Clamp(_dragValue.x, -_dragMaxDist, _dragMaxDist);
                _dragValue.y = Mathf.Clamp(_dragValue.y, -_dragMaxDist * _screenRatio, _dragMaxDist * _screenRatio);
            }
            else
            {
                _dragValue += _prevMouseOffset;
            }

                BasementCameraManager.Instance.OffsetCamera(_dragValue);
            _mouseOffsetTemp = _dragValue;
        }

        private bool CheckIngameObjectInteract(bool isPress)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(_input.MousePosition);
            mouseTarget = Physics2D.OverlapCircle(position, 0.1f, _whatIsBasementObject);

            if (mouseTarget != null &&
                mouseTarget.TryGetComponent(out IngameInteractiveObject interactObject))
            {
                interactObject.MouseEvent(isPress);
                _isMousePressed = false;
                return true;
            }

            return false;
        }

        #endregion

        #region MouseEventFeedback

        public void OnFocusRoom(BasementRoom room)
        {
            SetBasementColliderEnable(false);
            _isWideView = false;
            _currentRoom = room;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_basementRooms[i, j] == room)
                    {
                        _currentFloor = i;
                        _currentRoomNumber = j;

                        bool canGoLeft = (_currentRoomNumber > 0 && _basementRooms[i, j] != null);
                        bool canGoRight = (_currentRoomNumber < 2 && _basementRooms[i, j] != null);
                        //카페는 2칸을 차지해서 오른쪽으로는 못감
                        //if (_currentRoom is Cafe) canGoRight = false;

                        UIManager.Instance.basementUI.OnChangeRoom(canGoLeft, canGoRight);

                        return;
                    }
                }
            }
        }

        public void ReturnToWideView()
        {
            BasementCameraManager.Instance.ChangeOriginFollow(0.2f);
            BasementCameraManager.Instance.ZoomOut(0.2f);
            SetBasementColliderEnable(true);
            _mouseOffsetTemp = Vector2.zero;
            _prevMouseOffset = Vector2.zero;
            _isWideView = true;
        }

        private void SetBasementColliderEnable(bool isEnable)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_basementRooms[i, j] != null)
                    {
                        _basementRooms[i, j].SetColliderEnable(isEnable);
                    }
                }
            }
        }

        #endregion

        #region CameraMoveEvent

        //카메라 움직이기
        public void CameraMove(int x, int y)
        {
            if (_basementRooms[_currentFloor, _currentRoomNumber] != null)
                _basementRooms[_currentFloor, _currentRoomNumber].CloseUI();

            _currentFloor += y;
            _currentRoomNumber += x;

            if (_basementRooms[_currentFloor, _currentRoomNumber] != null)
            {
                _basementRooms[_currentFloor, _currentRoomNumber].FocusCamera();
                _basementRooms[_currentFloor, _currentRoomNumber].OpenRoomUI();
            }
        }

        //버튼 이벤트
        public void MoveLeft()
        {
            CameraMove(-1, 0);
        }

        public void MoveRight()
        {
            CameraMove(1, 0);
        }

        #endregion

        /// <summary>
        /// 토글에서 사용함
        /// </summary>
        /// <param name="isBuildMode"></param>
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

        /// <summary>
        /// 일단 임시로 만들어둔거지만 
        /// </summary>
        public void CompleteScadule()
        {
            //임시 코드임

            _timer.Close();
            _office.FocusRoom();
        }

        public void SetRoom(BasementRoom room, int floor, int roomNumber) => _basementRooms[floor, roomNumber] = room;
        public BasementMode GetCurrentBasementMode() => _currentMode;
    }

    public enum BasementMode
    {
        Basement,
        Build
    }
}
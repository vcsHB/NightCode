using System;
using UnityEngine;
using System.Collections.Generic;
using Basement.Player;
using DG.Tweening;
using System.Collections;
using Basement.CameraController;

namespace Basement
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private List<ElevatorFloorStruct> _floorStruct;
        [SerializeField] private Transform _elevatorCameraTarget;
        [SerializeField] private BasementPlayer _player;
        [SerializeField] private float _elevatorSpeed = 0.5f;
        private ElevatorDoor _prevDoor;
        private ElevatorDoor _targetDoor;
        private int _currentFloor = 3;
        private int _targetFloor = 0;
        private Sequence _seq;

        public BasementPlayer Player => _player;

        private void Awake()
        {
            _floorStruct.ForEach(elevator =>
            {
                elevator.door.Init(this);
                elevator.button.Init(_player, elevator.door, elevator.floor);
            });
        }

        private void OnDoorClosed()
        {
            var targetFloor = _floorStruct.Find(floor => floor.floor == _targetFloor);
            _targetDoor = targetFloor.door;

            //When Door Close Comletely Move Player Instantly
            _player.transform.position = targetFloor.door.transform.position;
            _prevDoor.onCompleteCloseDoor -= OnDoorClosed;

            Transform targetFollow = targetFloor.floorTarget;
            int floorDiff = Mathf.Abs(_currentFloor - _targetFloor);

            //Move Camera
            BasementCameraManager.Instance.ChangeFollow(targetFollow, floorDiff / _elevatorSpeed,
                () =>
                {
                    //When move complete open door of target floor
                    //When door open completly change floor and
                    Debug.Log(targetFloor.floor); 
                    _targetDoor.OpenDoor();
                    _targetDoor.onCompleteOpenDoor += OnDoorOpened;

                    //StartCoroutine(DelayAction(() =>
                    //{
                        
                    //}, 2f));
                });
        }

        private void OnDoorOpened()
        {
            _player.SetSortingLayer(5);
            _currentFloor = _targetFloor;

            _targetDoor.onCompleteOpenDoor -= OnDoorOpened;
        }

        public void ChangeFloor(ElevatorDoor prevDoor)
        {
            //이동하기 전의 엘리베이터 문을 닫아주고, 끝났을 때 액션 구독
            _prevDoor = prevDoor;
            _prevDoor.CloseDoor();
            _prevDoor.onCompleteCloseDoor += OnDoorClosed;

            //레이어를 바꿔서 엘리베이터에 탄거 같은 연출
            _player.SetSortingLayer(0);
        }



        public void SetTargetFloor(int targetFloor)
        {
            //목표 층 수를 지정했으면 엘리베이터 문이 열리게
            _targetFloor = targetFloor;
            _floorStruct.Find(floor => floor.floor == _currentFloor).door.OpenDoor();
        }

        private IEnumerator DelayAction(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }

    [Serializable]
    public struct ElevatorFloorStruct
    {
        public int floor;
        public ElevatorDoor door;
        public ElevatorButton button;
        public Transform floorTarget;
    }
}

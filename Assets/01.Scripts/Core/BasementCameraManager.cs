using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Basement.CameraController
{
    public class BasementCameraManager : MonoSingleton<BasementCameraManager>
    {
        public CinemachineCamera mainCamera;
        public int currentCameraTargetFloor;
        public bool isFocus = false;

        private CinemachineCamera _currentCamera;
        private CinemachineConfiner2D _confinder;
        private Vector3 _cameraOriginPos;

        private Tween _cameraChangeTween;
        private Tween _zoomTween;

        public float CameraSize => _currentCamera.Lens.OrthographicSize;

        protected override void Awake()
        {
            base.Awake();
            _currentCamera = mainCamera;
        }

        public void Zoom(float value, float duration = 0.2f, Ease ease = Ease.Linear)
        {
            if (_zoomTween != null && _zoomTween.active)
                _zoomTween.Kill();

            _zoomTween = DOTween.To(() => _currentCamera.Lens.OrthographicSize,
                x =>  _currentCamera.Lens.OrthographicSize = x,
                value, duration).SetEase(ease);
        }

        public void ChangeFollowToFloor(int floor, float duration, Action onComplete)
        {
            BasementSO basementSO = BasementManager.Instance.basementInfo;
            if (floor > basementSO.expendedFloor || floor < 0)
            {
                //여기서 카메라 좀 움직여서 안된다를 표현하시오
                return;
            }

            currentCameraTargetFloor = floor;
            //Transform target = BasementManager.Instance.floorCameraTarget[floor].transform;
            //ChangeFollow(target, duration, onComplete);
        }

        public void ChangeFollow(Transform target, float duration, Action onComplete, Ease easing = Ease.Linear)
        {
            if (target == null) return;

            if (_cameraChangeTween != null && _cameraChangeTween.active)
            {
                _cameraChangeTween.Kill();
            }
            else
            {
                _cameraOriginPos = _currentCamera.Follow.position;
            }

            _cameraChangeTween = _currentCamera.Follow.DOMove(target.position, duration)
                .SetEase(easing)
                .OnComplete(() =>
                {
                    _currentCamera.Follow.position = _cameraOriginPos;
                    _currentCamera.Follow = target;
                    onComplete?.Invoke();
                });
        }

        public Transform GetCameraFollow()
            => _currentCamera.Follow;

        //디버깅용
        private void Update()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
                Zoom(1);
            if (Keyboard.current.oKey.wasPressedThisFrame)
                Zoom(5);
        }
    }

}

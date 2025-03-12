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
        private Vector2 _originPos;
        private Vector2 _offset;

        private Tween _cameraMoveTween;
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
                x => _currentCamera.Lens.OrthographicSize = x,
                value, duration).SetEase(ease);
        }

        public void ChangeFollowToFloor(int floor, float duration, Action onComplete)
        {
            //BasementSO basementSO = BasementManager.Instance.basementInfo;
            //if (floor > basementSO.expendedFloor || floor < 0)
            //{
            //    //여기서 카메라 좀 움직여서 안된다를 표현하시오
            //    return;
            //}

            currentCameraTargetFloor = floor;
            //Transform target = BasementManager.Instance.floorCameraTarget[floor].transform;
            //ChangeFollow(target, duration, onComplete);
        }

        public void ChangeFollow(Transform target, float duration, Action onComplete, Ease easing = Ease.Linear)
        {
            if (target == null) return;

            if (_cameraMoveTween != null && _cameraMoveTween.active) _cameraMoveTween.Kill();
             _cameraOriginPos = _currentCamera.Follow.position - (Vector3)_offset;
            _offset = Vector2.zero;

            _cameraMoveTween = _currentCamera.Follow.DOMove(target.position, duration)
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

        public void OffsetCamera(Vector2 dragValue)
        {
            if (_cameraMoveTween != null && _cameraMoveTween.active)
                return;

            _offset = dragValue;
            _currentCamera.Follow.transform.position = _originPos + dragValue;
        }

        public void ResetCameraOffset()
        {
            if (_cameraMoveTween != null && _cameraMoveTween.active)
                _cameraMoveTween.Kill();

            _cameraMoveTween = DOTween.To(() => _offset, x => _offset = x, Vector2.zero, 0.2f)
                .OnUpdate(() => _currentCamera.Follow.position = _originPos + _offset);
            // = .DOMove(_originPos, 0.1f);
            //_offset = Vector2.zero;
        }

        //디버깅용
        private void Update()
        {
            //if (Keyboard.current.pKey.wasPressedThisFrame)
            //    Zoom(1);
            //if (Keyboard.current.oKey.wasPressedThisFrame)
            //    Zoom(5);
        }

        public void StartDrag()
            => _originPos = _currentCamera.Follow.position;
    }

}

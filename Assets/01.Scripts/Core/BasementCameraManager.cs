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
        private bool _isOffset = false;
        private Transform _originOffsetTrm;
        [SerializeField] private Transform _cameraOffsetFollow;
        public CinemachineCamera mainCamera;
        public int currentCameraTargetFloor;
        public bool isFocus = false;

        [SerializeField] private float _zoomOutSize;
        private CinemachineCamera _currentCamera;
        private CinemachineConfiner2D _confinder;

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

        public void ZoomOut(float duration = 0.2f, Ease ease = Ease.Linear)
            => Zoom(_zoomOutSize, duration, ease);


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

            if (_cameraMoveTween != null && _cameraMoveTween.active)
                _cameraMoveTween.Kill();

            if (_isOffset == false)
            {
                _isOffset = true;
                _originOffsetTrm = _currentCamera.Follow;
                _cameraOffsetFollow.position = _currentCamera.Follow.position;
                _currentCamera.Follow = _cameraOffsetFollow;
            }

            _cameraMoveTween = _cameraOffsetFollow.DOMove(target.position, duration)
                .SetEase(easing)
                .OnComplete(() =>
                {
                    _currentCamera.Follow = target;
                    onComplete?.Invoke();
                    _isOffset = false;
                });

        }

        public Transform GetCameraFollow()
        {
            if (_currentCamera == null)
                _currentCamera = mainCamera;

            return _currentCamera.Follow;
        }

        public void OffsetCamera(Vector2 dragValue)
        {
            //카메라가 움직이는 애니메이션 실행중일때는 오프셋을 적용 안 시켜줌
            if (_cameraMoveTween != null && _cameraMoveTween.active)
                return;

            if (_isOffset == false)
            {
                _isOffset = true;
                _originOffsetTrm = _currentCamera.Follow;
                _cameraOffsetFollow.position = _currentCamera.Follow.position;
                _currentCamera.Follow = _cameraOffsetFollow;
            }

            _cameraOffsetFollow.position = _originOffsetTrm.position + (Vector3)dragValue;
        }

        public void ResetCameraOffset()
        {
            if (_cameraMoveTween != null && _cameraMoveTween.active)
                _cameraMoveTween.Kill();

            _cameraMoveTween = _cameraOffsetFollow.DOMove(_originOffsetTrm.position, 0.2f)
                .OnComplete(() =>
                {
                    _currentCamera.Follow = _originOffsetTrm;
                    _isOffset = false;
                });

            // = .DOMove(_originPos, 0.1f);
            //_offset = Vector2.zero;
        }

        public Vector2 GetCameraOffset()
        {
            if (_isOffset == false) return Vector2.zero;
            return (_cameraOffsetFollow.position - _originOffsetTrm.position);
        }

        //디버깅용
        private void Update()
        {
            //if (Keyboard.current.pKey.wasPressedThisFrame)
            //    Zoom(1);
            //if (Keyboard.current.oKey.wasPressedThisFrame)
            //    Zoom(5);
        }
    }

}

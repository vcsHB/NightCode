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
        public Camera basementCamera;
        public Camera basementBuildCamera;

        private CameraMode _currentCameraMode;
        private CinemachineCamera _currentCamera;
        private CinemachineConfiner2D _confinder;

        private Tween _cameraChangeTween;
        private Tween _zoomTween;

        protected override void Awake()
        {
            base.Awake();
            _currentCamera = mainCamera;

            _currentCameraMode = CameraMode.Build;
            ChangeCameraMode(CameraMode.Basement);
        }

        public void ChangeCameraMode(CameraMode mode)
        {
            if (_currentCameraMode == mode) return;
            _currentCameraMode = mode;

            if (mode == CameraMode.Basement)
            {
                basementBuildCamera.gameObject.SetActive(false);
                basementCamera.gameObject.SetActive(true);
            }
            else
            {
                basementBuildCamera.gameObject.SetActive(true);
                basementCamera.gameObject.SetActive(false);
            }
        }

        public void Zoom(float value, float duration = 0.2f, Ease ease = Ease.Linear)
        {
            if (_zoomTween != null && _zoomTween.active)
                _zoomTween.Kill();

            _zoomTween = DOTween.To(() => _currentCamera.Lens.OrthographicSize,
                x =>
                {
                    _currentCamera.Lens.OrthographicSize = x;
                    basementBuildCamera.orthographicSize = x;
                    basementCamera.orthographicSize = x;
                },
                value, duration).SetEase(ease);
        }

        public void ChangeFollow(Transform target, float duration, Action onComplete, Ease easing = Ease.Linear)
        {
            if (target == null) return;

            if (_cameraChangeTween != null && _cameraChangeTween.active)
                _cameraChangeTween.Kill();

            _cameraChangeTween = _currentCamera.Follow.DOMove(target.position, duration)
                .SetEase(easing)
                .OnComplete(() =>
                {
                    _currentCamera.Follow = target;
                    onComplete?.Invoke();
                });
        }

        public Transform GetCameraFollow()
            => _currentCamera.Follow;

        //µð¹ö±ë¿ë
        private void Update()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
                Zoom(1);
            if (Keyboard.current.oKey.wasPressedThisFrame)
                Zoom(5);
        }
    }

    public enum CameraMode
    {
        Basement,
        Build
    }
}

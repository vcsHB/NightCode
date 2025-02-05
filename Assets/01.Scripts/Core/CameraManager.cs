using CameraControllers;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoSingleton<CameraManager>
{
    public List<CinemachineCamera> camList = new List<CinemachineCamera>();

    private CinemachineCamera _currentCamera;
    private CinemachineConfiner2D _confinder;

    private Tween _cameraChangeTween;
    private Tween _zoomTween;

    protected override void Awake()
    {
        base.Awake();
        _currentCamera = camList[0];
    }

    public void ChangeCamera(CinemachineCamera cam)
    {
        if (camList.Contains(cam) == false) return;

        _currentCamera = cam;
        _confinder = _currentCamera.GetComponent<CinemachineConfiner2D>();
    }

    public void Zoom(float value, float duration = 0.2f, Ease ease = Ease.Linear)
    {
        if (_zoomTween != null && _zoomTween.active)
            _zoomTween.Kill();

        _zoomTween = DOTween.To(() => _currentCamera.Lens.OrthographicSize,
            x => _currentCamera.Lens.OrthographicSize = x,
            value, duration).SetEase(ease);
    }

    public void ChangeFollow(Transform target, float duration, Action onComplete, Ease easing = Ease.Linear)
    {
        if (target == null) return;

        if(_cameraChangeTween != null && _cameraChangeTween.active)
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

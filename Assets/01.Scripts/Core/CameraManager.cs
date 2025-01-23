using DG.Tweening;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoSingleton<CameraManager>
{
    public List<CinemachineCamera> camList = new List<CinemachineCamera>();

    private CinemachineCamera _currentCamera;
    private CinemachineConfiner2D _confinder;

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

    public void Zoom(float value, float duration = 0.2f)
    {
        DOTween.To(() => _currentCamera.Lens.OrthographicSize, 
            x => _currentCamera.Lens.OrthographicSize = x,
            value, duration);
    }

    public void ChangeFollow(Transform target)
    {
        if( target == null ) return;

        _currentCamera.Follow = target;
    }

    //µð¹ö±ë¿ë
    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
            Zoom(1);
        if (Keyboard.current.oKey.wasPressedThisFrame)
            Zoom(5);
    }
}

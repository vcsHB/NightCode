using UnityEngine;
using Basement;
using Basement.Player;
using System.Collections;
using System;

public class ElevatorDoor : MonoBehaviour
{
    public Action onCompleteOpenDoor;
    public Action onCompleteCloseDoor;

    private Animator _animator;
    private Elevator _elevator;
    private Collider2D _collider;
    private int _floor;

    private int _doorOpenHash = Animator.StringToHash("Open");
    private int _doorCloseHash = Animator.StringToHash("Close");
    private bool _isDoorOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    private void OnInteract()
    {
        if (_isDoorOpen == false) return;
        _elevator.ChangeFloor();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(_doorOpenHash);
    }

    public void CompleteOpenDoor()
    {
        _isDoorOpen = true;
        _collider.enabled = true;
        onCompleteOpenDoor?.Invoke();
    }

    public void CompleteCloseDoor()
    {
        onCompleteCloseDoor?.Invoke();
    }

    public void CloseDoor()
    {
        _animator.SetTrigger(_doorCloseHash);
        _collider.enabled = false;
        _isDoorOpen = false;
    }


    public void Init(BasementPlayer player, Elevator elevator, int floor)
    {
        _elevator = elevator;
        _floor = floor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteract();
    }
}

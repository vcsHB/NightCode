using UnityEngine;
using Basement;
using Basement.Player;

public class ElevatorDoor : BasementObject
{
    private Animator _animator;
    private  BasementPlayer _basementPlayer;
    private int _floor;

    private int _doorOpenHash = Animator.StringToHash("Open");
    private int _doorCloseHash = Animator.StringToHash("Close");
    private bool _isDoorOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnInteract()
    {
        if (_isDoorOpen == false) return;
        _animator.SetTrigger(_doorCloseHash);
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(_doorOpenHash);
        _isDoorOpen = true;
    }

    public override void OnInteractObject()
    {
        if (_isDoorOpen == false) return;
        _basementPlayer.SetInteractAction(OnInteract);
    }

    public override void OnDeInteractObject()
    {
        _basementPlayer.RemoveInteractAction();
    }

    public void Init(BasementPlayer player, int floor)
    {
        _basementPlayer = player;
        _floor = floor;
    }
}

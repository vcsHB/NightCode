using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class IngameButton : MonoBehaviour
{
    public UnityEvent OnClickEvent;

    private bool _isMouseDown = false;

    private void LateUpdate()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
            _isMouseDown = false;
    }

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.05f;
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }


    public void OnMouseDown()
    {
        _isMouseDown = true;
    }

    public void OnMouseUp()
    {
        if (_isMouseDown == false) return;

        OnClickEvent?.Invoke();
    }
}

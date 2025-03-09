using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FloorSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private int _floor;
    private FloorSelectUI _selectUI; 

    public UnityEvent onClickEvent;
    public UnityEvent onPointerEnterEvent;

    private void Awake()
    {
        _selectUI = GetComponentInParent<FloorSelectUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectUI.SetFloor(_floor);
    }
}

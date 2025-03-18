using Basement;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildConfirmPanel : BasementCommonUI
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Button _confirmButton;

    public void SetRoom(BasementRoomSO roomSO, Action createRoomAction)
    {
        _nameText.SetText(roomSO.roomName);
        _coinText.SetText($"필요 코인: {roomSO.requireMoney}");

        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(() =>
        {
            createRoomAction?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public override void Open()
    {
        Debug.Log("밍");
        base.Open();
    }
}

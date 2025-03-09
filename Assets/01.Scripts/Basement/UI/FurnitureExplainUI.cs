using Basement;
using TMPro;
using UnityEngine;

public class FurnitureExplainUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _explainTxt;

    public void SetFurniture(FurnitureSO furniture)
    {
        gameObject.SetActive(true);
        _nameTxt.SetText(furniture.furnitureName);
        _explainTxt.SetText(furniture.furnitureDescription);
    }
}

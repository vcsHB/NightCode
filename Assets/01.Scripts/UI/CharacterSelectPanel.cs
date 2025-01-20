using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectPanel : MonoBehaviour, IUIPanel
{
    public delegate void CharacterSelectAction(CharacterType characterType);   

    public event CharacterSelectAction OnSelectCharacter;
    public CharacterSelectButton[] characterSelectBtn;

    private TrainingSO _trainingSO;
    [SerializeField] private TextMeshProUGUI _trainingName;
    [SerializeField] private TextMeshProUGUI _trainingExplain;
    [SerializeField] private TextMeshProUGUI _statExplain;

    private RectTransform RectTrm => transform as RectTransform;
    private RectTransform ImageRect => transform.GetChild(0) as RectTransform;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            CharacterType character = (CharacterType)i;
            characterSelectBtn[i].character = character;
            characterSelectBtn[i].OnClickEvent += OnClickBtn;
        }
    }

    public void SetSelectAction(CharacterSelectAction onCharacterSelect)
    {
        OnSelectCharacter = onCharacterSelect;
    }

    public void EnableSelectPanel(CharacterSelectAction onCharacterSelect , Vector2 position)
    {
    }

    public void OnClickBtn(CharacterType characterType)
    {
        OnSelectCharacter?.Invoke(characterType); 
        Close();
    }

    public void Open(Vector2 position)
    {
        Vector2 size = ImageRect.sizeDelta;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        position.x = Mathf.Clamp(position.x, -screenSize.x / 2, screenSize.x / 2 - size.x);
        position.y = Mathf.Clamp(position.y, -screenSize.y / 2 + size.y, screenSize.y / 2);

        RectTrm.anchoredPosition = position;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetTrainingSO(TrainingSO training)
    {
        _trainingName.SetText(training.trainingName);
        _trainingExplain.SetText(training.trainingExplain);
        _statExplain.SetText($"¼º°ø È®·ü\t{training.successChance}%\t{training.statType} +{training.successValue}\n´ë¼º°ø È®·ü\t{training.greatSuccesChance / 100f}%\t{training.statType} +{training.greatSuccessValue}"); 
    }
}

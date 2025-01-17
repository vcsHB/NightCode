using System.Collections.Generic;
using UnityEngine;

public class TrainingPanal : MonoBehaviour
{
    public Dictionary<CharacterType, int> characterHealth;

    [SerializeField] private CharacterSelectPanel _selectPanel;
    private Training[] trainings;

    private void Awake()
    {
        InitializeCharacterHealth();
        trainings = GetComponentsInChildren<Training>();

        foreach (Training training in trainings)
        {
            training.Init(_selectPanel);
        }
    }

    public void InitializeCharacterHealth()
    {
        characterHealth = new Dictionary<CharacterType, int>();
        //나중에 이거 관련된거 정확히 기획이 되면 그때 저장, 불러오기 같은거도 가져와

        int healthInitailizeValue = 7;
        characterHealth.Add(CharacterType.Katana, healthInitailizeValue);
        characterHealth.Add(CharacterType.CrescentBlade, healthInitailizeValue);
        characterHealth.Add(CharacterType.Cross, healthInitailizeValue);
    }

    public int GetCharacterHealt(CharacterType character)
        => characterHealth[character];

    public bool TryUseCharacterHealth(CharacterType character, int value)
    {
        if (characterHealth[character] < value) return false;

        characterHealth[character] -= value;
        return true;
    }
}

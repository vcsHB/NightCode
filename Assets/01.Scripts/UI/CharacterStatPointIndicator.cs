using GGM.Core.StatSystem;
using TMPro;
using UnityEngine;
using Basement.Training;

public class CharacterStatPointIndicator : MonoBehaviour
{
    public TextMeshProUGUI _characterName;

    public TextMeshProUGUI _intelligence;
    public TextMeshProUGUI _strength;
    public TextMeshProUGUI _agility;

    public void SetCharacter(CharacterEnum characterType)
    {
        _characterName.SetText(characterType.ToString());
        
        //CharacterStat stat = AgentStatManager.Instance.GetStatPoint(characterType);
        //_intelligence.SetText($"Intelligence / {stat.intelligence}");
        //_strength.SetText($"Strength / {stat.strength}");
        //_agility.SetText($"Agility / {stat.dexterity}");
    }
}

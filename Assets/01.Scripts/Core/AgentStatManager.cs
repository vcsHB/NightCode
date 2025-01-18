using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentStatManager : MonoSingleton<AgentStatManager>
{
    public Dictionary<CharacterType, CharacterStat> characterStatPoint;

    protected override void Awake()
    {
        base.Awake();

        characterStatPoint = new Dictionary<CharacterType, CharacterStat>();
        foreach (CharacterType character in Enum.GetValues(typeof(CharacterType)))
        {
            characterStatPoint.Add(character, new CharacterStat());
        }
    }

    public void AddStatPoint(CharacterType character, StatType statType, int value)
    {
        CharacterStat stat = characterStatPoint[character];
        
        switch(statType)
        {
            case StatType.Strength:
                stat.strength += value;
                break;
            case StatType.Intelligence:
                stat.intelligence += value;
                break;
            case StatType.Agility:
                stat.agility += value;
                break;
        }

        characterStatPoint[character] = stat;
    }
}

public enum CharacterType
{
    Katana,
    CrescentBlade,
    Cross
}

public enum StatType
{
    Strength,
    Intelligence,
    Agility
}

public struct CharacterStat
{
    public int strength;
    public int intelligence;
    public int agility;
}

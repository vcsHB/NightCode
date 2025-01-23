using GGM.Core.StatSystem;
using System;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

public class AgentStatManager : MonoSingleton<AgentStatManager>
{
    public CharacterStat katanaStat;
    public CharacterStat crescentBladeStat;
    public CharacterStat crossStat;

    public Dictionary<CharacterType, CharacterStat> characterStatPoint;

    protected override void Awake()
    {
        base.Awake();

        characterStatPoint = new Dictionary<CharacterType, CharacterStat>();

        foreach (CharacterType character in Enum.GetValues(typeof(CharacterType)))
            characterStatPoint.Add(character, new CharacterStat());
    }


    public void AddStat(CharacterType character, StatType statType, string key, int value)
    {
        switch (character)
        {
            case CharacterType.Katana:
                katanaStat.AddStat(statType, key, value);
                break;
            case CharacterType.CrescentBlade:
                 crescentBladeStat.AddStat(statType, key, value);
                break;
            case CharacterType.Cross:
                 crossStat.AddStat(statType, key, value);
                break;
        }
    }

    public void AddStatPoint(CharacterType character, StatType statType, string key, int value)
        => characterStatPoint[character].AddStat(statType, key, value);


    public CharacterStat GetStat(CharacterType character)
    {
        switch (character)
        {
            case CharacterType.Katana:
                return katanaStat;
            case CharacterType.CrescentBlade:
                return crescentBladeStat;
            case CharacterType.Cross:
                return crossStat;
        }

        return katanaStat;
    }

    public CharacterStat GetStatPoint(CharacterType character) => characterStatPoint[character];
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
    Dexterity
}

[Serializable]
public class CharacterStat
{
    public StatSO strength;
    public StatSO intelligence;
    public StatSO dexterity;

    public void AddStat(StatType statType, string key, int value)
    {
        switch (statType)
        {
            case StatType.Strength:
                strength.AddModifier(key, value);
                break;
            case StatType.Intelligence:
                intelligence.AddModifier(key, value);
                break;
            case StatType.Dexterity:
                dexterity.AddModifier(key, value);
                break;
        }
    }
}
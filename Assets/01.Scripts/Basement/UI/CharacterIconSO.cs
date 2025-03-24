using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CharacterIconSO")]
public class CharacterIconSO : ScriptableObject
{
    public List<IconStruct> iconList;

    public Sprite GetIcon(CharacterEnum character)
        => iconList.Find(iconStruct => iconStruct.character == character).icon;
}

[Serializable]
public struct IconStruct
{
    public CharacterEnum character;
    public Sprite icon;
}

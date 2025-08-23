using Agents.Players.ChipsetSystem;
using Core.Attribute;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{

    public enum ChipsetType
    {
        Personal,
        Global
    }

    [CreateAssetMenu(fileName = "ChipsetSO", menuName = "SO/Chipset/ChipsetSO")]
    public class ChipsetSO : ScriptableObject
    {
        [ReadOnly] public ushort id;

        [Header("Display Information")]
        public string chipsetName;
        [TextArea] public string chipsetDescription;

        [Header("Function Setting")]
        public ChipsetType chipsetType;
        public ChipsetFunction chipsetFunctionPrefab;
        public Sprite icon;

        [Space]
        [Header("Add position from leftbottom of each slot")]
        public List<Vector2Int> chipsetSize;
        public Chipset chipsetPrefab;
        public bool isRotatable;
    }
}

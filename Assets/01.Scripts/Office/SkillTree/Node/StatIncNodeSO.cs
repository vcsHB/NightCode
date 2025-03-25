using StatSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StatIncreaseNode")]
public class StatIncNodeSO : NodeSO
{
    public StatIncrease[] stat;
}
public struct StatIncrease
{
    public StatSO statType;
    public float increaseValue;
}


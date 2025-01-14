using UnityEngine;

[CreateAssetMenu(menuName = "SO/Training")]
public class TrainingSO : ScriptableObject
{
    public string trainingName;
    public StatType statType;

    [Header("Chance is between 0 ~ 100")]
    [Space(5)]

    //ÈÆ·Ã ¼º°ø È®·ü
    public float successChance;
    public int successValue;
    
    [Space(15)]
    
    //ÈÆ·Ã ´ë¼º°ø È®·ü
    public float greatSuccesChance;
    public int greatSuccessValue;
}

public enum TrainingResult
{
    Fail,
    Success,
    GreatSuccess
}
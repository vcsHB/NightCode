using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Training")]
public class TrainingSO : ScriptableObject
{
    public string trainingName;
    public string trainingExplain;
    public StatType statType;

    [Header("Chance is between 0 ~ 100")]
    [Space(5)]

    public int failValue;
    public Color failColor;

    [Space(15)]

    //ÈÆ·Ã ¼º°ø È®·ü
    public float successChance;
    public int successValue;
    public Color successColor;
    
    [Space(15)]
    
    //ÈÆ·Ã ´ë¼º°ø È®·ü
    public float greatSuccesChance;
    public int greatSuccessValue;
    public Color greatSuccessColor;

    public Dictionary<TrainingResult, int> increaseValue;
    public Dictionary<TrainingResult, Color> textColor;

    private void OnEnable()
    {
        increaseValue = new Dictionary<TrainingResult, int>();
        textColor = new Dictionary<TrainingResult, Color>();

        increaseValue.Add(TrainingResult.Fail, failValue);
        increaseValue.Add(TrainingResult.Success, successValue);
        increaseValue.Add(TrainingResult.GreatSuccess, greatSuccessValue);

        textColor.Add(TrainingResult.Fail, failColor);
        textColor.Add(TrainingResult.Success, successColor);
        textColor.Add(TrainingResult.GreatSuccess, greatSuccessColor);
    }
}

public enum TrainingResult
{
    Fail,
    Success,
    GreatSuccess
}
using System.Collections.Generic;
using UnityEngine;

namespace Basement.Training
{
    [CreateAssetMenu(menuName = "SO/Basement/Training")]
    public class TrainingSO : ScriptableObject
    {
        public int trainingId;
        public string trainingName;

        public string trainingVisibleName;
        public string trainingExplain;
        public SkillPointEnum statType;
        [Header("Unit is minute")]
        public int requireTime;

        [Header("Chance is between 0 ~ 100")]
        [Space(5)]
        [Header("Fail Chance is 100 - SuccessChance\nGreatSuccess Chance Calculate After Success")]
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

        [Space(15)]
        public int requireFatigue;

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

        public TrainingResult GetResult(CharacterEnum characterType)
        {
            float fatigueCorrection = (100 - CharacterManager.Instance.GetFatigue(characterType)) / 100;

            TrainingResult result = Random.Range(0, 101) > successValue * fatigueCorrection ? TrainingResult.Fail :
                Random.Range(0, 101) > greatSuccessValue * fatigueCorrection ? TrainingResult.Success : TrainingResult.GreatSuccess;

            return result;
        }

        public TrainingSO GetInstance()
        {
            TrainingSO training = ScriptableObject.CreateInstance<TrainingSO>();

            training.trainingId = trainingId;
            training.trainingName = trainingName;
            training.trainingVisibleName = trainingVisibleName;
            training.trainingExplain = trainingExplain;
            training.statType = statType;
            training.requireTime = requireTime;
            training.failValue = failValue;
            training.failColor = failColor;
            training.successChance = successChance;
            training.successColor = successColor;
            training.successValue = successValue;
            training.greatSuccesChance = greatSuccesChance;
            training.greatSuccessColor = greatSuccessColor;
            training.greatSuccessValue = greatSuccessValue;
            training.requireFatigue = requireFatigue;

            return training;
        }
    }

    public enum TrainingResult
    {
        Fail,
        Success,
        GreatSuccess
    }
}
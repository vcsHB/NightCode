using Basement.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Basement.Training
{
    [CreateAssetMenu(fileName = "TrainingSetSO", menuName = "SO/Basement/TrainingSetSO")]
    public class TrainingSetSO : ScriptableObject
    {
        public List<TrainingSO> trainingSO;

        //Level은 1레벨 부터
        public TrainingSO GetTrainingSO(string trainingName)
            => trainingSO.Find(training => training.trainingName == trainingName);
    }
}

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

        public TrainingSO GetTrainingSO(int trainingId)
            => trainingSO.Find(training => training.trainingId == trainingId);
    }
}

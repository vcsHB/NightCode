using System;
using UnityEngine;

namespace Tutorial
{

    public class GuideObject : MonoBehaviour
    {
        [SerializeField] private GoalObject _goal;
        [SerializeField] private ShowPointArea _showPoint;
        [SerializeField] private DescriptionBox _description;
        private void Awake()
        {
            _goal.OnGoalArrivedEvent.AddListener(HandleArriveGoal);
            _showPoint.OnGoalArrivedEvent.AddListener(HandleShowPoint);
        }

        private void HandleShowPoint()
        {
            _description.Open();
        }

        private void HandleArriveGoal()
        {
            _description.Close();

        }
    }

}
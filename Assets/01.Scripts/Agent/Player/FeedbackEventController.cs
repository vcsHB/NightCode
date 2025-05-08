using System.Collections.Generic;
using Core.EventSystem;
using FeedbackSystem;
using UnityEngine;

namespace Agents
{

    public class FeedbackCreateEventData : GameEvent
    {
        public string feedbackName;
        public FeedbackCreateEventData(string feedbackName)
        {
            this.feedbackName = feedbackName;
        }
    }

    public class FeedbackFinishEventData : GameEvent
    {
        public string feedbackName;
        public FeedbackFinishEventData(string feedbackName)
        {
            this.feedbackName = feedbackName;
        }
    }
    public class FeedbackEventController : MonoBehaviour, IAgentComponent
    {
        private GameEventChannelSO _feedbackEventChannel;
        private Dictionary<string, FeedbackPlayer> _feedbackPlayerDictionary;
        private Agent _owner;
        private void Awake()
        {
            // 딕셔너리 채우기
            _feedbackPlayerDictionary = new();
            FeedbackPlayer[] players = GetComponentsInChildren<FeedbackPlayer>();
            for (int i = 0; i < players.Length; i++)
            {
                FeedbackPlayer feedback = players[i];
                _feedbackPlayerDictionary.Add(feedback.gameObject.name, feedback);
            }


        }
        private void OnDestroy()
        {
            _feedbackEventChannel.RemoveListener<FeedbackCreateEventData>(HandleInvokeFeedbacks);
            _feedbackEventChannel.RemoveListener<FeedbackFinishEventData>(HandleFinishFeedbacks);
        }

        public void Initialize(Agent agent)
        {
            _owner = agent;
            _feedbackEventChannel = _owner.EventChannel;

            _feedbackEventChannel.AddListener<FeedbackCreateEventData>(HandleInvokeFeedbacks);
            _feedbackEventChannel.AddListener<FeedbackFinishEventData>(HandleFinishFeedbacks);
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }


        private void HandleInvokeFeedbacks(FeedbackCreateEventData data)
        {
            Invoke(data.feedbackName);
        }
        private void HandleFinishFeedbacks(FeedbackFinishEventData data)
        {
            Finish(data.feedbackName);
        }

        public void Invoke(string feedbackName)
        {
            FeedbackPlayer feedback = GetFeedback(feedbackName);
            if (feedback == null) return;
            feedback.PlayFeedback();
        }

        public void Finish(string feedbackName)
        {
            FeedbackPlayer feedback = GetFeedback(feedbackName);
            if (feedback == null) return;
            feedback.FinishFeedback();
        }

        private FeedbackPlayer GetFeedback(string feedbackName)
        {
            if (_feedbackPlayerDictionary.TryGetValue($"{feedbackName}Feedback", out FeedbackPlayer feedbackPlayer))
            {
                return feedbackPlayer;
            }
            return null;
        }


    }

}
using System.Collections.Generic;
using Core.EventSystem;
using FeedbackSystem;
using UnityEngine;

namespace Agents.Players
{

    public class FeedbackEventData : GameEvent
    {
        public string feedbackName;
    }
    public class FeedbackEventController : MonoBehaviour, IAgentComponent
    {
        private GameEventChannelSO _feedbackCreateEventChannel;
        private GameEventChannelSO _feedbackFinishEventChannel;
        private Dictionary<string, FeedbackPlayer> _feedbackPlayerDictionary;
        private Player _player;

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
            _feedbackCreateEventChannel.RemoveListener<FeedbackEventData>(HandleInvokeFeedbacks);
            _feedbackFinishEventChannel.RemoveListener<FeedbackEventData>(HandleFinishFeedbacks);
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _feedbackCreateEventChannel = _player.CreateFeedbackChannel;
            _feedbackFinishEventChannel = _player.FinishFeedbackChannel;

            _feedbackCreateEventChannel.AddListener<FeedbackEventData>(HandleInvokeFeedbacks);
            _feedbackFinishEventChannel.AddListener<FeedbackEventData>(HandleFinishFeedbacks);
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }


        private void HandleInvokeFeedbacks(FeedbackEventData data)
        {
            Invoke(data.feedbackName);
        }
        private void HandleFinishFeedbacks(FeedbackEventData data)
        {
            Finish(data.feedbackName);
        }

        public void Invoke(string feedbackName)
        {
            if (_feedbackPlayerDictionary.TryGetValue(feedbackName, out FeedbackPlayer feedbackPlayer))
            {
                feedbackPlayer.PlayFeedback();
            }
        }

        public void Finish(string feedbackName)
        {
            if (_feedbackPlayerDictionary.TryGetValue(feedbackName, out FeedbackPlayer feedbackPlayer))
            {
                feedbackPlayer.FinishFeedback();
            }
        }


    }

}
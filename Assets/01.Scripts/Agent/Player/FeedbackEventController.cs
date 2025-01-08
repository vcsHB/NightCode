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
    public class FeedbackEventController : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _feedbackCreateEventChannel;
        [SerializeField] private GameEventChannelSO _feedbackFinishEventChannel;
        private Dictionary<string, FeedbackPlayer> _feedbackPlayerDictionary;

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

            _feedbackCreateEventChannel.AddListener<FeedbackEventData>(HandleInvokeFeedbacks);
            _feedbackFinishEventChannel.AddListener<FeedbackEventData>(HandleFinishFeedbacks);
        }


        private void OnDestroy()
        {
            _feedbackCreateEventChannel.RemoveListener<FeedbackEventData>(HandleInvokeFeedbacks);
            _feedbackFinishEventChannel.RemoveListener<FeedbackEventData>(HandleFinishFeedbacks);
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
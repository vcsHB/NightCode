using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EventSystem
{
    public class GameEvent
    {
        //empty class
    }
    
    [CreateAssetMenu(fileName = "GameEventChannelSO", menuName = "SO/Events/GameEventChannelSO")]
    public class GameEventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> _events = new();
        private Dictionary<Delegate, Action<GameEvent>> _lookUp = new();

        public void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            if (_lookUp.ContainsKey(handler) == false)  //이미 구독중인 매서드는 다시 구독되지 않도록
            {
                //타입캐스트 핸들러 작성
                Action<GameEvent> castHandler = (evt) => handler(evt as T);
                _lookUp[handler] = castHandler;

                Type evtType = typeof(T);
                if (_events.ContainsKey(evtType))
                {
                    _events[evtType] += castHandler;
                }
                else
                {
                    _events[evtType] = castHandler;
                }
            }
            else
            {
                Debug.Log("이미 구독중");
            }
        }

        public void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            Type evtType = typeof(T);
            if (_lookUp.TryGetValue(handler, out Action<GameEvent> action))
            {
                if (_events.TryGetValue(evtType, out Action<GameEvent> internalAction))
                {
                    internalAction -= action;
                    if (internalAction == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = internalAction;
                }
                _lookUp.Remove(handler);
            }
        }

        public void RaiseEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> handlers))
            {
                handlers?.Invoke(evt);
            }
        }

        public void Clear()
        {
            _events.Clear();
            _lookUp.Clear();
        }
    }
}

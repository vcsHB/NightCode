using System;
using System.Collections.Generic;
using System.Linq;
using Agents.Players;
using Combat;
using Core.EventSystem;
using UnityEngine;

namespace Agents
{
    public struct Min
    {

    }
    public abstract class Agent : MonoBehaviour
    {
        public event Action OnDieEvent;
        [field: SerializeField] public GameEventChannelSO EventChannel { get; protected set; }

        public Health HealthCompo { get; protected set; }
        public PlayerCombatEnergyController EnergyController { get; protected set; }
        public bool IsDead { get; protected set; }
        private Dictionary<Type, IAgentComponent> _components = new Dictionary<Type, IAgentComponent>();

        protected virtual void Awake()
        {
            EventChannel = Instantiate(EventChannel);
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleAgentDie);

            AddComponentToDictionary();
            ComponentInitialize();
            AfterInit();
            HealthCompo.Initialize();
            EnergyController = GetCompo<PlayerCombatEnergyController>();
        }

        protected virtual void Start()
        {

        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IAgentComponent>(true)
                .ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        private void AfterInit()
        {
            _components.Values.ToList().ForEach(compo => compo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_components.TryGetValue(typeof(T), out IAgentComponent compo))
            {
                return compo as T;
            }
            else
            {
                // 상속구조의 예외 발생 가능 
                //Debug.Log("Not Exist In components dictionary");
                T newComponent = GetComponentInChildren<T>();
                if (newComponent is IAgentComponent)
                {

                    _components.Add(typeof(T), newComponent as IAgentComponent);
                    //Debug.Log("Insert In dictionary");
                    return newComponent;
                }
            }

            if (!isDerived) return default;

            Type findType = _components.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _components[findType] as T;

            return default(T);
        }

        protected virtual void HandleAgentDie()
        {
            if (IsDead) return;
            IsDead = true;
            OnDieEvent?.Invoke();
        }


    }

}
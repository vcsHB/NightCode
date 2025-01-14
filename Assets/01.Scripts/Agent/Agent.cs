using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agents
{
    public class Agent : MonoBehaviour
    {

        private Dictionary<Type, IAgentComponent> _components = new Dictionary<Type, IAgentComponent>();

        protected virtual void Awake()
        {
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInit();
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
            }else
            {
                // 상속구조의 예외 발생 가능 
                Debug.Log("상속구조상 예외가 발생되었지..");
                T newComponent =  GetComponentInChildren<T>();
                if(newComponent is IAgentComponent)
                {
                    Debug.Log("하지만 iAgent");

                    _components.Add(typeof(T), newComponent as IAgentComponent);
                    return newComponent;
                }
            }

            if (!isDerived) return default;

            Type findType = _components.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _components[findType] as T;

            return default(T);
        }

    }

}
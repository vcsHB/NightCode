using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.VolumeControlSystem
{

    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _globalVolume;
        private Dictionary<Type, VolumeController> _controllers = new();



        protected override void Awake()
        {
            base.Awake();
            Initialize();

        }
        private void Initialize()
        {
            GetComponentsInChildren<VolumeController>(true)
               .ToList().ForEach(controller => _controllers.Add(controller.GetType(), controller));


            foreach(var controller in _controllers.Values)
            {
                controller.Initialize(_globalVolume);
            }
        }



        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_controllers.TryGetValue(typeof(T), out VolumeController compo))
            {
                return compo as T;
            }

            if (!isDerived) return default;

            Type findType = _controllers.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _controllers[findType] as T;

            return default(T);
        }

    }
}
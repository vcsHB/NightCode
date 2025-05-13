using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialog
{
    public static class DialogActorManager
    {
        public static Dictionary<string, Actor> actorDic = new();


        public static void AddActor(string key, Actor actor)
        {
            if(actorDic.ContainsKey(key))
            {
                Debug.LogWarning($"actor name of {key} is arleady exsist.\nbut you still trying to add actor with key {key}");
                return;
            }

            actorDic.Add(key, actor);
        }

        public static void RemoveActor(string key, Actor actor)
        {
            if (actorDic.ContainsKey(key))
                actorDic.Remove(key);
        }


        public static bool TryGetActor(string key, out Actor actor)
        {

            return actorDic.TryGetValue(key, out actor);
        }
    }
}

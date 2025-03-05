using Agents.Animate;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Basement.NPC
{
    [CreateAssetMenu(menuName = "SO/Basement/NPC")]
    public class NPCSO : ScriptableObject
    {
        public Vector2 idleTime;
        public Vector2 roamingTime;
        public float speed;

        [Range(0, 100)]
        public float expressEmotionChance;
        public List<AnimParamSO> npcEmotionList;

        public AnimParamSO GetRandomEmotion()
        {
            float random = Random.value * 100;
            if (random > expressEmotionChance) return null;

            int idx = npcEmotionList.Count;
            return npcEmotionList[Random.Range(0, idx)];
        }

        public float GetRandomIdleTime()
            => Random.Range(idleTime.x, idleTime.y);

        public float GetRandomRoamingTime()
            => Random.Range(roamingTime.x, roamingTime.y);
    }
}

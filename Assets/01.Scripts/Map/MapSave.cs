using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class MapSave
    {
        public int seed;

        public int currentChapter;
        public int enterStageId;
        public Vector2Int enterStagePosition;

        public List<Vector2Int> completedNodes;

        public bool isEnteredStageClear;
        public bool isFailStageClear;

        public MapSave()
        {
            System.Random random = new System.Random();

            seed = random.Next() / 100000;
            currentChapter = 0;
            enterStageId = 0;
            completedNodes = new List<Vector2Int>();
        }
    }
}

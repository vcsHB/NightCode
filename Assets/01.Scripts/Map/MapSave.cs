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

        public bool isEnteredStageClear = false;
        public bool isFailStageClear = false;

        public MapSave()
        {
            seed = 0;
            currentChapter = 0;
            enterStageId = 0;
            completedNodes = new List<Vector2Int>();
        }
    }
}

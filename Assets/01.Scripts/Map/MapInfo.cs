using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapInfo
    {
        public List<MapNodeSO>[] map;
        public List<List<Vector2Int>>[] connections;
    }
}

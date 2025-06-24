using Core.DataControl;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public event Action<MapNode> OnClickNodeEvent;
        public MapGraphSO mapGraphSO;
        public MapNode nodePrefab;

        [Header("Map Generate")]
        [SerializeField] private List<HeightInfo> _heights;
        [SerializeField] private float _xOffset;
        [SerializeField] private Transform _nodeParent;
        [SerializeField] private Transform _lineParent;

        private string _path = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private MapSave _save;

        private MapGraph _mapGraph;
        private MapCharacterController _characterController;

        #region Property

        public MapGraph MapGraph => _mapGraph;
        public int CurrentChapter { get; private set; }
        public int CurrentDepth { get; private set; }

        #endregion


        public void Initialize(List<Vector2Int> characterPosition)
        {
            _mapGraph = new MapGraph();
            _characterController = GetComponent<MapCharacterController>();

            //Get Data Seed, CharacterPosition, Map Clear Fail information
            Load();

            //This must happen after Load
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                _mapGraph.characterOriginPosition.Add(character, characterPosition[(int)character]);
                _mapGraph.characterCurrentPosition.Add(character, characterPosition[(int)character]);
            }

            //_mapGraph will be init in GenerateNode Func
            GenerateNode();
            _characterController.Init(_mapGraph);

            _save.completedNodes.ForEach(SetCompleteNode);
            SetCompleteNode(Vector2Int.zero);
        }

        public void InitializeData()
        {
            if (_save == null) _save = new MapSave();
            CurrentDepth = 0;

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                _mapGraph.characterOriginPosition.Add(character, Vector2Int.zero);
                _mapGraph.characterCurrentPosition.Add(character, Vector2Int.zero);
            }

            _save.seed = UnityEngine.Random.Range(0, 1000000);
            Save();
        }

        #region MapGenerate

        private void GenerateNode()
        {
            if (_save == null) Load();
            UnityEngine.Random.InitState(_save.seed);

            MapInfo mapInfo = mapGraphSO.GenerateMap();
            int depth = mapInfo.map.Length;

            List<MapNode>[] nodes = new List<MapNode>[depth];

            for (int i = 0; i < depth; i++)
            {
                int height = mapInfo.map[i].Count;
                List<MapNode> nodeList = new List<MapNode>();
                for (int j = 0; j < height; j++)
                {
                    MapNode node = Instantiate(nodePrefab, _nodeParent);
                    nodeList.Add(node);

                    float x = i * _xOffset;
                    float y = _heights[height - 1].positions[j];
                    node.RectTrm.anchoredPosition = new Vector2(x, y);

                    if (i < mapInfo.connections.Length) node.nextPositions = mapInfo.connections[i][j];
                    node.onSelectNode += HandleSelectNode;
                    node.onPointerEnter += HandleSelectCharacterIcon;
                    node.Init(mapInfo.map[i][j], i, j);
                }
                nodes[i] = nodeList;
            }

            ConnectNode(nodes);
            _mapGraph.Init(nodes);
        }

        private void ConnectNode(List<MapNode>[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = 0; j < nodes[i].Count; j++)
                {
                    nodes[i][j].nextPositions.ForEach(next =>
                    {
                        MapNode nextNode = nodes[next.x][next.y];
                        nodes[i][j].ConnectEdge(nextNode, _lineParent);
                    });
                }
            }
        }

        private void HandleSelectNode(MapNode data)
        {
            //움직이지 않은 캐릭터가 있는지 확인
            bool isCompleteMove = true;
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_mapGraph.IsCharacterExsists(character) == false) continue;
                if (_characterController.GetIcon(character).IsCompleteCurerntLevel == false) break;
                if (_characterController.GetIcon(character).IsMoved == false)
                {
                    isCompleteMove = false;
                    break;
                }
            }
            if (isCompleteMove == false) return;

            OnClickNodeEvent?.Invoke(data);
        }

        private void HandleSelectCharacterIcon(MapNode node)
        {
            _characterController.HandleSelectCharacterIcon(node);
        }

        #endregion

        #region Save&Load

        public void Save()
        {
            if (_save == null) _save = new MapSave();

            string json = JsonUtility.ToJson(_save, true);
            File.WriteAllText(_path, json);
        }

        public bool Load()
        {
            if (File.Exists(_path) == false)
            {
                InitializeData();
                Save();
                return false;
            }

            string json = File.ReadAllText(_path);
            _save = JsonUtility.FromJson<MapSave>(json);

            CurrentDepth = int.MaxValue;

            if (_save.isEnteredStageClear)
            {
                _save.completedNodes.Add(_save.enterStagePosition);
            }
            _save.isEnteredStageClear = false;
            _save.isFailStageClear = false;
            return true;
        }

        public void SaveEnterStage(MapNode node)
        {
            if (_save == null) _save = new MapSave();
            _save.enterStageId = node.NodeInfo.nodeId;      // 인게임에서 맵 불러오기 위한
            _save.enterStagePosition = node.Position;       // 맵 선택 씬에서 진행중인 씬 확을 위한
            Save();
        }

        public void SetCompleteNode(Vector2Int nodePosition)
        {
            _mapGraph.GetNode(nodePosition).CompleteNode();

            bool isComplete = true;
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_mapGraph.IsCharacterExsists(character))
                {
                    Vector2Int position = _mapGraph.GetCharcterCurrentPosition(character);
                    if (_mapGraph.GetNode(position).IsComplete == false) isComplete = false;
                }
            }

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_mapGraph.IsCharacterExsists(character))
                {
                    _characterController.GetIcon(character).SetCompleteCurrentLevel(isComplete);
                }
            }
        }

        #endregion
    }
}


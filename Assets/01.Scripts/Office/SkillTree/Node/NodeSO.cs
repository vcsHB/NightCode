using System;
using System.Collections.Generic;
using UnityEngine;

namespace Office.CharacterSkillTree
{
    //[CreateAssetMenu(menuName = "SO/SkillTreeNode")]
    public abstract class NodeSO : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        public int id;
        public List<NodeSO> nextNodes;
        public NodeSO prevNode;
        public List<NodeSO> exceptNodes;


        public string fileName;
        public string nodeName;
        [TextArea]
        public string explain;
        public Sprite icon;
        public int requireCoin;
        public Action onValueChange;

        private void OnValidate()
        {
            onValueChange?.Invoke();
            name = fileName;
            exceptNodes.ForEach(except => except.AddExcept(this));

            for(int i = 0; i < exceptNodes.Count - 1; i++)
            {
                for(int j = i + 1; j < exceptNodes.Count; j++)
                {
                    if (exceptNodes[i] == exceptNodes[j]) 
                        exceptNodes.RemoveAt(j--);
                }
            }
        }

        private void AddExcept(NodeSO nodeSO)
        {
            if (exceptNodes == null) exceptNodes = new();

            if(exceptNodes.Contains(nodeSO) == false)
                exceptNodes.Add(nodeSO);
        }
    }
}

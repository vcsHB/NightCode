using System;
using Map;
using TMPro;
using UnityEngine;
namespace UI.NodeViewScene.StageSelectionUIs
{

    public class StageSelectionPanel : MonoBehaviour
    {
        [SerializeField] private MapGraph _mapGraph;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;


        private void Awake()
        {
            _mapGraph.OnSelectNodeEvent += HandleSelectMapNode;
        }

        private void HandleSelectMapNode(MapNodeSO data)
        {
            _titleText.text = data.displayName;
            _descriptionText.text = data.explain;
            
        }
    }
}
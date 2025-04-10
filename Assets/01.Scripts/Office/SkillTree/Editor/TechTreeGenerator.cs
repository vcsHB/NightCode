using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Office.CharacterSkillTree
{
    public class TechTreeGenerator : EditorWindow
    {
        private TechTreeGraphView graphView;
        private InspectorView inspectorView;

        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("Tools/TechTreeGenerator")]
        public static void ShowExample()
        {
            TechTreeGenerator wnd = GetWindow<TechTreeGenerator>();
            wnd.titleContent = new GUIContent("TechTreeGenerator");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is SkillTreeSO)
            {
                ShowExample();
                return true;
            }
            return false;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            m_VisualTreeAsset.CloneTree(root);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/Office/SkillTree/Editor/TechTreeGenerator.uss");
            root.styleSheets.Add(styleSheet);

            graphView = root.Q<TechTreeGraphView>();
            inspectorView = root.Q<InspectorView>();

            graphView.OnNodeSelected = OnNodeSelectionChanged;

            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            SkillTreeSO dialog = Selection.activeObject as SkillTreeSO;
            if (dialog)
                graphView.ParpurateView(dialog);
        }

        private void OnNodeSelectionChanged(NodeView view)
        {
            inspectorView.UpdateSelection(view);
        }
    }
}

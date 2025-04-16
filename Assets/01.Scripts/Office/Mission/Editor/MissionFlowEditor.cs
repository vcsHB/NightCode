using Office;
using Office.CharacterSkillTree;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Office
{
    public class MissionFlowEditor : EditorWindow
    {
        private MissionGraphView graphView;
        private InspectorView inspectorView;

        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("Tools/MissionFlowGenerator")]
        public static void ShowExample()
        {
            MissionFlowEditor wnd = GetWindow<MissionFlowEditor>();
            wnd.titleContent = new GUIContent("MissionFlowEditor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is MissionSetSO)
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

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/Office/Mission/Editor/MissionFlowEditor.uss");
            root.styleSheets.Add(styleSheet);

            graphView = root.Q<MissionGraphView>();
            inspectorView = root.Q<InspectorView>();

            graphView.OnNodeSelected = OnNodeSelectionChanged;

            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            MissionSetSO mission = Selection.activeObject as MissionSetSO;
            if (mission)
                graphView.ParpurateView(mission);
        }

        private void OnNodeSelectionChanged(MissionNodeView view)
        {
            inspectorView.UpdateSelection(view);
        }
    }
}

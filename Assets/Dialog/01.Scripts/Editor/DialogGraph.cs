#if UNITY_EDITOR
using Dialog;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;


namespace Dialog
{
    public class DialogGraph : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private DialogView _dialogView;
        private InspectorView _inspectorView;
        private InspectorView _conditionInspector;

        private SerializedObject _dialogObj;

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is DialogSO)
            {
                ShowEditor();
                return true;
            }
            return false;
        }

        [MenuItem("Tools/DialogGenerator")]
        private static void ShowEditor()
        {
            DialogGraph wnd = GetWindow<DialogGraph>();
            wnd.titleContent = new GUIContent("JINSOON DIALOG");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            VisualElement content = m_VisualTreeAsset.Instantiate();
            content.style.flexGrow = 1;
            root.Add(content);

            _dialogView = content.Q<DialogView>("dialog-view");
            _inspectorView = content.Q<InspectorView>("inspector-view");
            _conditionInspector = content.Q<InspectorView>("condition-view");

            _dialogView.OnNodeSelected += HandleNodeSlect;
            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            var dialogSO = Selection.activeObject as DialogSO;

            if (dialogSO == null)
            {
                if (Selection.activeGameObject)
                {
                    var player = Selection.activeGameObject.GetComponent<DialogPlayer>();

                    if (player != null)
                    {
                        dialogSO = player.dialog;
                    }
                }
            }

            if (dialogSO != null)
                _dialogObj = new SerializedObject(dialogSO);

            if (Application.isPlaying)
            {
                if (dialogSO != null)
                    _dialogView?.PopulateTree(dialogSO);
            }
            else
            {
                if (dialogSO != null && AssetDatabase.CanOpenAssetInEditor(dialogSO.GetInstanceID()))
                    _dialogView?.PopulateTree(dialogSO);
            }
        }

        private void HandleNodeSlect(NodeView view)
        {
            _inspectorView.UpdateSelection(view);

            if (view.nodeSO is BranchNodeSO branch)
            {
                _conditionInspector.UpdateSelection(branch.condition);
            }
            else
            {
                _conditionInspector.ClearSelection();
            }
        }

        //private void OnInspectorUpdate()
        //{
        //    _dialogView?.UpdateNodeState();
        //}
    }
}


#endif
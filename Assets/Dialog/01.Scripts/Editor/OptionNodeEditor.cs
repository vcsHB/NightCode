
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace Dialog
{
    [CustomEditor(typeof(OptionNodeSO))]
    public class OptionNodeEditor : Editor
    {
        public SerializedProperty _options;
        public SerializedProperty _optionPf;
        private GUIStyle _textAreaStyle;
        public ReorderableList optionList;


        private int _selected = -1;
        private bool _isOptionOpen = false;

        private void OnEnable()
        {
            StyleSetup();
            OptionNodeSO optionSO = (OptionNodeSO)target;

            optionList = new ReorderableList(
                serializedObject,
                serializedObject.FindProperty("options"),
                true, true, true, true);
            _optionPf = serializedObject.FindProperty("optionPf");
            _options = serializedObject.FindProperty("options");

            //15 + 5 + 70 + 5 + 15 + 5
            optionList.elementHeight = 115;
            optionList.drawElementCallback = (rect, index, active, focused) =>
            {
                StyleSetup();

                var element = optionList.serializedProperty.GetArrayElementAtIndex(index);
                var optionProp = element.FindPropertyRelative("option");

                Rect labelRect = new Rect(rect.x, rect.y, rect.width, 15);
                Rect txtAreaRect = new Rect(rect.x, rect.y + 20, rect.width, 70);
                Rect nextNodeRect = new Rect(rect.x, rect.y + 95, rect.width, 15);

                EditorGUI.LabelField(labelRect, $"Option-{index}");
                optionProp.stringValue = EditorGUI.TextArea(txtAreaRect, optionProp.stringValue, _textAreaStyle);
            };

            optionList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Options");
            optionList.onAddCallback = list => optionSO.AddOption();
            optionList.onRemoveCallback = list =>
            {
                if (optionSO.options.Count >= 0)
                {
                    //선택된게 없으면
                    if (list.index == -1)
                    {
                        //제일 뒤에있는거를 지워라
                        optionSO.RemoveOption(optionSO.options.Count - 1);
                    }
                    else
                    {
                        //선택된거를 지워라
                        optionSO.RemoveOption(list.index);
                    }
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            optionList.DoLayoutList();
            EditorGUILayout.PropertyField(_optionPf);
            serializedObject.ApplyModifiedProperties();
        }

        private void StyleSetup()
        {
            if (_textAreaStyle == null)
            {
                _textAreaStyle = new GUIStyle(EditorStyles.textArea);
                _textAreaStyle.wordWrap = true;
            }
        }
    }
}

#endif
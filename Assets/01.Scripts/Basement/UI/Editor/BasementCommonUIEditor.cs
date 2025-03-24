using UnityEditor;
using static Basement.BasementCommonUI;

namespace Basement
{
    [CustomEditor(typeof(BasementCommonUI))]
    public class BasementCommonUIEditor : Editor
    {
        private SerializedProperty connectedUIList;
        private SerializedProperty linkedUI;
        private SerializedProperty oppositeUI;
        private SerializedProperty _openWithConnectedUI, _closeWithConnectedUI;
        private SerializedProperty _openAfterConnectedUICloseAnim, _closeAfterConnectedUICloseAnim;
        private SerializedProperty _openAfterLinkedUICloseAnim, _closeAfterLinkedUICloseAnim;

        private SerializedProperty canvasGroup;
        private SerializedProperty closeOnStart;

        private SerializedProperty tweenStartValue, tweenEndValue;
        private SerializedProperty tweenStartPos, tweenEndPos;
        private SerializedProperty tweenDuration;

        private BasementCommonUI buildUI = null;

        private void OnEnable()
        {
            _openWithConnectedUI = serializedObject.FindProperty("_openWithConnectedUI");
            _closeWithConnectedUI = serializedObject.FindProperty("_closeWithConnectedUI");
            _openAfterConnectedUICloseAnim = serializedObject.FindProperty("_openAfterConnectedUICloseAnim");
            _closeAfterConnectedUICloseAnim = serializedObject.FindProperty("_closeAfterConnectedUICloseAnim");
            _openAfterLinkedUICloseAnim = serializedObject.FindProperty("_openAfterLinkedUICloseAnim");
            _closeAfterLinkedUICloseAnim = serializedObject.FindProperty("_closeAfterLinkedUICloseAnim");

            connectedUIList = serializedObject.FindProperty("connectedUIList");
            linkedUI = serializedObject.FindProperty("linkedUI");
            oppositeUI = serializedObject.FindProperty("oppositeUI");

            closeOnStart = serializedObject.FindProperty("closeOnStart");
            canvasGroup = serializedObject.FindProperty("canvasGroup");
            tweenStartValue = serializedObject.FindProperty("tweenStartValue");
            tweenEndValue = serializedObject.FindProperty("tweenEndValue");
            tweenStartPos = serializedObject.FindProperty("tweenStartPos");
            tweenEndPos = serializedObject.FindProperty("tweenEndPos");
            tweenDuration = serializedObject.FindProperty("tweenDuration");

            if (AssetDatabase.Contains(target) == false)
                buildUI = target as BasementCommonUI;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PropertyField(connectedUIList);
                EditorGUILayout.PropertyField(linkedUI);
                EditorGUILayout.PropertyField(oppositeUI);

                EditorGUILayout.BeginVertical("HelpBox");
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(_openWithConnectedUI);
                        EditorGUILayout.PropertyField(_closeWithConnectedUI);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(_openAfterConnectedUICloseAnim);
                        EditorGUILayout.PropertyField(_closeAfterConnectedUICloseAnim);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(_openAfterLinkedUICloseAnim);
                        EditorGUILayout.PropertyField(_closeAfterLinkedUICloseAnim);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();



                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(closeOnStart);
                buildUI.animType = (OpenCloseAnimType)EditorGUILayout.EnumPopup("AnimType", buildUI.animType);

                switch (buildUI.animType)
                {
                    case OpenCloseAnimType.MoveTweenX:
                    case OpenCloseAnimType.MoveTweenY:
                        {
                            EditorGUILayout.BeginHorizontal("HelpBox");
                            {
                                EditorGUILayout.PropertyField(tweenStartValue);
                                EditorGUILayout.PropertyField(tweenEndValue);
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.PropertyField(tweenDuration);
                            break;
                        }
                    case OpenCloseAnimType.MoveTweenXY:
                        {
                            EditorGUILayout.PropertyField(tweenStartPos);
                            EditorGUILayout.PropertyField(tweenEndPos);
                            EditorGUILayout.PropertyField(tweenDuration);
                            break;
                        }
                    case OpenCloseAnimType.Fade:
                        {
                            EditorGUILayout.PropertyField(canvasGroup);

                            EditorGUILayout.BeginHorizontal("HelpBox");
                            {
                                EditorGUILayout.PropertyField(tweenStartValue);
                                EditorGUILayout.PropertyField(tweenEndValue);
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.PropertyField(tweenDuration);
                            break;
                        }
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }

}

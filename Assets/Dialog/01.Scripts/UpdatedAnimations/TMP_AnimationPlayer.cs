using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialog.Animation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMP_AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private List<TextAnimationSO> _animationSOList;
        [SerializeField] private string testText;

        private TextMeshProUGUI _tmp;
        private TMP_TextInfo _textInfo;

        private string _originText;
        private string _parsedText;
        private List<TextAnimationInfo> _animations = new();
        private List<CharacterData> _characterDatas = new();
        private int _currentEnabledIndex = 0;

        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            _textInfo = _tmp.textInfo;

            SetText(testText);
            DisableAllText();
        }

        private void Update()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                EnableText();
            }
        }

        public void DisableAllText()
        {
            for (int i = 0; i < _textInfo.characterInfo.Length; i++)
            {
                _textInfo.characterInfo[i].isVisible = false;
            }
        }

        public void EnableText()
        {
            _textInfo.characterInfo[_currentEnabledIndex++].isVisible = true;
        }

        public void SetText(string text)
        {
            _originText = text;
            _parsedText = text;

            _animations = TagParser.ParseAnimation(ref _parsedText, _animationSOList);
            _tmp.SetText(_parsedText);
            _tmp.ForceMeshUpdate();

            SetCharacterInfo();
        }


        //TODO FIX CharacterIno Setting
        private void SetCharacterInfo()
        {
            Debug.Log(_textInfo.characterCount + " " + _textInfo.meshInfo[0].vertices.Length);
            _characterDatas = new List<CharacterData>();
            for (int i = 0; i < _textInfo.characterCount; i++)
            {
                TMP_CharacterInfo characterinfo = _textInfo.characterInfo[i];

                MeshData meshData = new();
                meshData.positions = new Vector3[4];
                meshData.colors = new Color32[4];

                for (int j = 0; j < 4; j++)
                {
                    meshData.positions[j] = _textInfo.meshInfo[0].vertices[(i * 4) + j];
                    meshData.colors[j] = _textInfo.meshInfo[0].colors32[(i * 4) + j];
                }

                CharacterData characterData = new();
                characterData.isVisible = characterinfo.isVisible;
                characterData.source = meshData;
                characterData.current = meshData;
                characterData.timer = 0;

                _characterDatas.Add(characterData);
            }
        }


        private void LateUpdate()
        {
            TMP_TextInfo textInfo = _tmp.textInfo;

            Debug.Log(_characterDatas.Count);
            _animations.ForEach(animationInfo =>
            {
                Debug.Log(animationInfo.start + " " + animationInfo.end);
                for (int i = animationInfo.start; i < 2; i++)
                {
                    TMP_CharacterInfo characterinfo = textInfo.characterInfo[i];

                    bool isEnable = characterinfo.isVisible;

                    _characterDatas[i].isVisible = isEnable;
                    if (isEnable) _characterDatas[i].timer += Time.deltaTime;

                    animationInfo.animSO.ApplyEffortToCharacter(_characterDatas[i], this);
                }
            });

            Vector3[] positions = new Vector3[_characterDatas.Count * 4];
            Color32[] colors = new Color32[_characterDatas.Count * 4];
            for (int i = 0; i < _characterDatas.Count; ++i)
            {
                for (int j = 0; j < 4; j++)
                {
                    positions[i * 4 + j] = _characterDatas[i].current.positions[j];
                    colors[i * 4 + j] = _characterDatas[i].current.colors[j];
                }
            }

            var meshInfo = _textInfo.meshInfo[0];
            meshInfo.mesh.vertices = positions;
            meshInfo.mesh.colors32 = colors;
            _tmp.UpdateGeometry(meshInfo.mesh, 0);
        }
    }
}

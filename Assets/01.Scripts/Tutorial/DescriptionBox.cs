using System.Collections;
using TMPro;
using UnityEngine;
namespace Tutorial
{

    public class DescriptionBox : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textCompo;
        private string _content;
        [SerializeField] private float _printTerm = 0.1f;
        [SerializeField] private float _removeTerm = 0.06f;
        private WaitForSeconds _waitForSec;
        private WaitForSeconds _removeWaitForSec;

        private void Awake()
        {
            _textCompo.maxVisibleCharacters = 0;
            _waitForSec = new WaitForSeconds(_printTerm);
            _removeWaitForSec = new WaitForSeconds(_removeTerm);
        }

        public void Open()
        {
            StartCoroutine(OpenCoroutine());
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }
        private IEnumerator OpenCoroutine()
        {
            TMP_TextInfo textInfo = _textCompo.textInfo;
            _textCompo.maxVisibleCharacters = 0;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                _textCompo.maxVisibleCharacters++;
                yield return _waitForSec;
            }
        }

        private IEnumerator CloseCoroutine()
        {
            TMP_TextInfo textInfo = _textCompo.textInfo;
            _textCompo.maxVisibleCharacters = textInfo.characterCount;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                _textCompo.maxVisibleCharacters--;
                yield return _removeWaitForSec;
            }
        }

        private void OnValidate()
        {
            if (_textCompo == null) return;
            _content = _textCompo.text;
        }

    }
}
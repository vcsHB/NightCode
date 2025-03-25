using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.InGame.GameUI.CallTalk
{
    [System.Serializable]
    public struct TalkData
    {
        public string sender;
        public string content;

    }
    public class CallTalkUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _senderNameText;
        [SerializeField] private TextMeshProUGUI _contentText;
        [SerializeField] private float _textPrintTerm = 0.04f;
        [SerializeField] private float _endWaitDisableTerm = 3f;
        private WaitForSeconds _waitForTerm;
        private WaitForSeconds _endTerm;
        private Coroutine _talkCoroutine;
        private AudioSource _audioSource;
        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            _waitForTerm = new WaitForSeconds(_textPrintTerm);
            _endTerm = new WaitForSeconds(_endWaitDisableTerm);
        }


        public void SetNewTalk(TalkData newData)
        {
            if (_talkCoroutine != null) StopCoroutine(_talkCoroutine);
            _audioSource.PlayOneShot(_audioSource.clip);
            SetCanvasActiveImmediately(true);
            _senderNameText.text = newData.sender;
            _contentText.text = newData.content;
            _talkCoroutine = StartCoroutine(TalkCoroutine());
        }
        private IEnumerator TalkCoroutine()
        {
            TMP_TextInfo textInfo = _contentText.textInfo;
            _contentText.maxVisibleCharacters = 0;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                _contentText.maxVisibleCharacters++;
                yield return _waitForTerm;
            }
            yield return _endTerm;
            _talkCoroutine = null;
            Close();

        }
    }

}
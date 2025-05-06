using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    public class MSGText : MonoBehaviour
    {
        [SerializeField] private MSGTextBox _textBox;
        [SerializeField] private int _maxTextBoxNum;
        [SerializeField] private CharacterIconSO icon;

        private MSGTextBox prevTextBox;
        private Stack<MSGTextBox> _textBoxPool;
        private Queue<MSGTextBox> _exsistBox;

        private void Awake()
        {
            _textBoxPool = new Stack<MSGTextBox>();
            _exsistBox = new Queue<MSGTextBox>();

            for (int i = 0; i < _maxTextBoxNum; i++)
            {
                MSGTextBox textBox = Instantiate(_textBox, transform);
                textBox.gameObject.SetActive(false);
                _textBoxPool.Push(textBox);
            }
        }

        private void Update()
        {
            //if (Keyboard.current.pKey.wasPressedThisFrame)
            //    PopMSGText(null, "심장훈 매우 섹시함");
            //if(Keyboard.current.oKey.wasPressedThisFrame)
            //    PopMSGText(testIcon, "심장훈 매우 섹시함");
        }

        public void PopMSGText(CharacterEnum character, string text, int rating)
        {
            PopMSGText(icon.GetIcon(character), text, rating);
        }

        public void PopMSGText(Sprite icon, string text, int rating)
        {
            if (_textBoxPool.TryPeek(out MSGTextBox textBox))
            {
                textBox.gameObject.SetActive(true);
                textBox.Init(icon, text, this, prevTextBox, rating);

                prevTextBox = textBox;
            }
            else
            {
                Push();
                PopMSGText(icon, text, rating);
            }
        }

        public void Push()
        {
            if (_exsistBox.TryDequeue(out MSGTextBox textBox))
            {
                textBox.Init(null, "", this, null, 0);
                textBox.gameObject.SetActive(false);
                _textBoxPool.Push(textBox);
            }
        }
    }
}

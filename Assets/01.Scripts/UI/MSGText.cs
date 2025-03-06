using Basement.Training;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Basement
{
    public class MSGText : MonoBehaviour
    {
        [SerializeField] private MSGTextBox _textBox;
        [SerializeField] private int _maxTextBoxNum;

        [SerializeField] private List<Sprite> icon;

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

        public void PopMSGText(CharacterEnum character, string text)
        {
            PopMSGText(icon[(int)character],text);
        }

        public void PopMSGText(Sprite icon, string text)
        {
            if (_textBoxPool.TryPop(out MSGTextBox textBox))
            {
                textBox.gameObject.SetActive(true);
                textBox.Init(icon, text, this, prevTextBox);

                prevTextBox = textBox;
                _exsistBox.Enqueue(textBox);
            }
            else
            {
                Push();
                PopMSGText(icon, text);
            }
        }

        public void Push()
        {
            MSGTextBox textBox = _exsistBox.Dequeue();
            textBox.gameObject.SetActive(false);
            _textBoxPool.Push(textBox);
        }
    }
}

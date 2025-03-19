using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialog
{
    public class OptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Action<NodeSO> OnClcickEvent;
        [SerializeField] private TextMeshProUGUI _txt;

        private AnimationPlayer _player;
        private List<TagAnimation> _tagAnims = new List<TagAnimation>();
        private NodeSO _nextNode;

        public void SetOption(Option optionStruct, AnimationPlayer player)
        {
            _txt.SetText(optionStruct.optionTxt);
            _tagAnims = optionStruct.optionTagAnimations;
            _nextNode = optionStruct.nextNode;
            _player = player;
        }

        private void LateUpdate()
        {
            if (_player == null) return;

            _player.PlayAnimation(_txt, _tagAnims);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClcickEvent?.Invoke(_nextNode);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }
    }
}

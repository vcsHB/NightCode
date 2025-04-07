using System;
using UnityEngine;

namespace Office.InteractSystem
{


    public class HoverSelectOutline : MonoBehaviour
    {
        private InteractionTarget _targetOwner;
        private SpriteRenderer _visualRenderer;
        private readonly int _isOutLinePropertyHash = Shader.PropertyToID("_IsOutline");
        private void Awake()
        {

            _visualRenderer = GetComponentInChildren<SpriteRenderer>();
            _targetOwner = GetComponent<InteractionTarget>();
            _targetOwner.OnHoverEnterEvent.AddListener(HandlHoverSelected);
            _targetOwner.OnHoverExitEvent.AddListener(HandleHoverExited);
        }


        private void HandlHoverSelected()
        {
            SetOutline(true);

        }
        private void HandleHoverExited()
        {
            SetOutline(false);
        }

        private void SetOutline(bool value)
        {
            _visualRenderer.material.SetInt(_isOutLinePropertyHash, value ? 1 : 0);
        }
    }

}
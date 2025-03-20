using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    public class OmeletRiceMiniGame : MonoBehaviour
    {
        public RectTransform ketchupBottleRect;
        public RectTransform tipRect;

        public Transform pixelParent;

        private bool _isPressed;
        private Vector2 screenSize = new Vector2(Screen.width / 2, Screen.height / 2);

        private bool _checkPrevPosition = false;
        private Vector2 _prevPosition;

        private void Awake()
        {
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _checkPrevPosition = false;
                _isPressed = true;
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame) _isPressed = false;

            if (_isPressed) DrawLine(3);
        }

        private void DrawLine(int width)
        {
            Vector2 pixelPosition = (Mouse.current.position.value - screenSize);

            pixelPosition.x = (float)Mathf.Round(pixelPosition.x / 10) * 10f;
            pixelPosition.y = (float)Mathf.Round(pixelPosition.y / 10) * 10f;

            if (_checkPrevPosition)
            {
                //위치에 2칸이상 차이가 나고 있다면
                if (Vector2.Distance(_prevPosition, pixelPosition) < 20) return; //* width) return;

                Vector2 correctionDir = (pixelPosition - _prevPosition).normalized;
                Vector2 correctionPosition = _prevPosition;

                while(Vector2.Distance(correctionPosition, pixelPosition) > 10)
                {
                    correctionPosition += correctionDir * 10;
                    correctionPosition.x = (float)Mathf.Round(correctionPosition.x / 10) * 10f;
                    correctionPosition.y = (float)Mathf.Round(correctionPosition.y / 10) * 10f;

                    SetPixel(correctionPosition);
                }
            }

            SetPixel(pixelPosition);
        }

        private void SetPixel(Vector2 pixelPosition)
        {

        }

    }
}

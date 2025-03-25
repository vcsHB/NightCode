using DG.Tweening;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    public class OmeletRiceMiniGame : MonoBehaviour
    {
        public event Action<bool> onCompleteMiniGame;
        public CafeInput input;

        public PaintTexture paintTexture;
        public PaintTexture guideLineTexture;

        public RectTransform ketchupTip;
        public ParticleSystem ketchupParticle;
        public RectTransform goodResult, badResult;

        #region PrivateVariables

        [SerializeField] private string _fileName;
        private float _duration = 0.3f;
        private bool _isOpened;
        private bool _isPressed;
        private string _directoryPath = "MinigameInfo";
        private string _path;

        private Tween _openCloseTween, _resultTween;
        private List<Vector2> _pixelPositions = new List<Vector2>();
        private List<Vector2> _guidLinePositions = new List<Vector2>();
        private bool _checkPrevPosition = false;
        private bool _isEndDrawing = false;
        private bool _isGood = false;
        private Vector2 _prevPosition;

        #endregion


        public RectTransform RectTrm => transform as RectTransform;
        private Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);


        private void Start()
        {
            input.onLeftClick += OnLeftClick;
        }

        private void OnDisable()
        {
            input.onLeftClick -= OnLeftClick;
        }


        private void Update()
        {
            if (_isPressed)
            {
                DrawLine(3);
                ketchupTip.anchoredPosition = Mouse.current.position.value - screenPosition;
            }
        }


        private void OnLeftClick(bool isPressed)
        {
            if (_isOpened == false) return;

            if (_isEndDrawing && isPressed)
            {
                Close();
                return;
            }

            _isPressed = isPressed;

            if (isPressed)
            {
                _checkPrevPosition = false;

                ketchupTip.anchoredPosition = Mouse.current.position.value - screenPosition;
                ketchupParticle.Play();
            }
            else
            {
                CompareCompletion();
                _isEndDrawing = true;
            }
        }


        private void DrawLine(int width)
        {
            if (width % 2 == 0) width++;

            Vector2 pixelPosition = Mouse.current.position.value;
            ConvertPositionTo10(ref pixelPosition);

            if (_checkPrevPosition)
            {
                //위치에 2칸이상 차이가 나고 있다면

                int xDiff = Mathf.RoundToInt((pixelPosition.x - _prevPosition.x) / 10f);
                int yDiff = Mathf.RoundToInt((pixelPosition.y - _prevPosition.y) / 10f);

                float xProgress = 0;
                float yProgress = 0;
                int trial = Mathf.Max(Mathf.Abs(xDiff), Mathf.Abs(yDiff));

                for (int i = 0; i < trial; i++)
                {
                    xProgress += xDiff / (float)trial;
                    yProgress += yDiff / (float)trial;

                    Vector2 correction = new Vector2(Mathf.Round(xProgress), Mathf.Round(yProgress)) * 10;
                    SetPixel(_prevPosition + correction, width);
                }
            }

            _checkPrevPosition = true;
            _prevPosition = pixelPosition;
            SetPixel(pixelPosition, width);
        }


        private void ConvertPositionTo10(ref Vector2 position)
        {
            position.x = (float)Mathf.Round(position.x / 10) * 10f;
            position.y = (float)Mathf.Round(position.y / 10) * 10f;
        }


        private void SetPixel(Vector2 pixelPosition, int width)
        {
            int half = width / 2;
            for (int i = -half; i <= half; i++)
            {
                for (int j = -half; j <= half; j++)
                {
                    if (Mathf.Abs(i) + Mathf.Abs(j) > half) continue;

                    Vector2 correctionPosition = pixelPosition + new Vector2(i, j) * 10;
                    correctionPosition = paintTexture.ConvertPosition(correctionPosition);
                    if (_pixelPositions.Exists(p => correctionPosition == p)) continue;

                    _pixelPositions.Add(correctionPosition);
                    paintTexture.DrawTexture(correctionPosition);
                }
            }
        }


        public void SetGuideLine(string fileName)
        {
            string path = Path.Combine(_directoryPath, fileName);
            string json = Resources.Load<TextAsset>(path).ToString();
            TexturePixelInfo info = JsonUtility.FromJson<TexturePixelInfo>(json);

            guideLineTexture.ResetTexture();
            _guidLinePositions = info.positions;
            _guidLinePositions.ForEach(position => guideLineTexture.DrawTexture(position));
        }



        public void Open()
        {
            _isOpened = true;
            _isEndDrawing = false;

            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(0f, _duration);
            input.DisableInput();
        }

        private void Close()
        {
            _isOpened = false;

            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(1080f, _duration);
            paintTexture.ResetTexture();
            _pixelPositions.Clear();
            input.EnableInput();

            if (_isGood)
            {
                if (_resultTween != null && _resultTween.active)
                    _resultTween.Kill();

                _resultTween = goodResult.DOScale(0f, _duration).SetEase(Ease.InSine);
            }
            else
            {
                if (_resultTween != null && _resultTween.active)
                    _resultTween.Kill();

                _resultTween = badResult.DOScale(0f, _duration).SetEase(Ease.InSine);
            }
        }

        public void CompareCompletion()
        {
            int missed = 0;
            int overflowed = 0;

            _pixelPositions.ForEach(pos =>
            {
                if (_guidLinePositions.Exists(p => p == pos) == false)
                    overflowed++;
            });

            _guidLinePositions.ForEach(pos =>
            {
                if (_pixelPositions.Exists(p => p == pos) == false)
                    missed++;
            });


            if (_resultTween != null && _resultTween.active)
                _resultTween.Kill();

            //못함
            if (150 < missed + overflowed)
            {
                _resultTween = badResult.DOScale(1f, _duration).SetEase(Ease.InSine);
                _isGood = false;
            }
            //잘함
            else
            {
                _resultTween = goodResult.DOScale(1f, _duration).SetEase(Ease.InSine);
                _isGood = true;
            }

            onCompleteMiniGame?.Invoke(_isGood);
        }


#if UNITY_EDITOR

        [ContextMenu("SaveTexture")]
        public void SaveTexture()
        {
            _path = Path.Combine(_directoryPath, _fileName);
            TexturePixelInfo info = new TexturePixelInfo();
            info.positions = _pixelPositions;

            string json = JsonUtility.ToJson(info);
            File.WriteAllText(_path, json);
        }

#endif
    }

    [Serializable]
    public class TexturePixelInfo
    {
        public List<Vector2> positions;
    }
}

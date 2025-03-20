using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Cafe
{
    public class UIPixelDrawing : MonoBehaviour
    {
        private CanvasRenderer _canvasRenderer;
        private List<Vector2> _vertices;
        [SerializeField] private Color _lineColor;
        [SerializeField] private float _pixelWidth;

        private void Awake()
        {
            _canvasRenderer = GetComponent<CanvasRenderer>();
            _vertices = new List<Vector2>();

        }

        public void SetPixel(Vector2 position)
        {
            // vertex gen
            AddVertexTrial(position - Vector2.one * _pixelWidth / 2);
            AddVertexTrial(position + Vector2.one * _pixelWidth / 2);
            AddVertexTrial(position + new Vector2(_pixelWidth / 2, -_pixelWidth / 2));
            AddVertexTrial(position + new Vector2(-_pixelWidth / 2, _pixelWidth / 2));

            // triangle gen


            // texture gen


            // sprite gen
        }

        public void GenerateSprite()
        {

        }

        private void AddVertexTrial(Vector2 position)
        {
            if (_vertices.Exists(vert => vert == position)) return;
            _vertices.Add(position);
        }
    }
}

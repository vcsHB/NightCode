using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UIPixelDrawer : MaskableGraphic
    {
        private List<Vector2> _vertices;
        private List<ushort> _triangles;
        [SerializeField] private Color _lineColor;
        [SerializeField] private float _pixelWidth;
        private Image _image;

        protected override void Awake()
        {
            base.Awake();

            _image = GetComponent<Image>();
            _vertices = new List<Vector2>();
            _triangles = new List<ushort>();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

        }

        public void SetPixel(Vector2 position)
        {
            ushort index = (ushort)_vertices.Count;

            // vertex gen
            _vertices.Add(position + new Vector2(-_pixelWidth / 2, _pixelWidth / 2));
            _vertices.Add(position - Vector2.one * _pixelWidth / 2);
            _vertices.Add(position + new Vector2(_pixelWidth / 2, -_pixelWidth / 2));
            _vertices.Add(position + Vector2.one * _pixelWidth / 2);


            // triangle gen
            _triangles.Add(index);
            _triangles.Add((ushort)(index + 1));
            _triangles.Add((ushort)(index + 2));

            _triangles.Add(index);
            _triangles.Add((ushort)(index + 2));
            _triangles.Add((ushort)(index + 3));


            // texture gen
            Texture2D texture = new Texture2D(10, 10);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    texture.SetPixel(i, j, _lineColor);
                }
            }
            texture.Apply();

            // sprite gen
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 1f);
            sprite.OverrideGeometry(_vertices.ToArray(), _triangles.ToArray());

            _image.sprite = sprite;
        }
    }
}

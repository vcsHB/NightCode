using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
 * - 그림 그릴 대상 게임오브젝트에 컴포넌트로 넣기
 * 
 */
public class PaintTexture : MonoBehaviour
{
    public int resolution = 512;
    public float brushSize = 10f;
    public Texture2D brushTexture;
    public Color color;

    private Texture2D _mainTex;
    private RawImage _image;
    private RenderTexture _rt;

    private RectTransform RectTrm => transform as RectTransform;
    private Vector2 screenSizeHalf => new Vector2(Screen.width / 2, Screen.height / 2);

    private void Awake()
    {
        TryGetComponent(out _image);
        _rt = new RenderTexture(resolution, resolution, 32);

        if (_image.texture != null)
        {
            _mainTex = _image.texture as Texture2D;
        }
        // 메인 텍스쳐가 없을 경우, 하얀 텍스쳐를 생성하여 사용
        else
        {
            _mainTex = new Texture2D(resolution, resolution);
        }

        // 메인 텍스쳐 -> 렌더 텍스쳐 복제
        Graphics.Blit(_mainTex, _rt);

        // 렌더 텍스쳐를 메인 텍스쳐에 등록
        _image.texture = _rt;

        // 브러시 텍스쳐가 없을 경우 임시 생성(red 색상)
        if (brushTexture == null)
        {
            brushTexture = new Texture2D(resolution, resolution);
            for (int i = 0; i < resolution; i++)
                for (int j = 0; j < resolution; j++)
                    brushTexture.SetPixel(i, j, color);
            brushTexture.Apply();
        }
    }

    public Vector2 ConvertPosition(Vector2 position)
    {
        position -= RectTrm.anchoredPosition + screenSizeHalf;
        position += new Vector2(RectTrm.rect.width / 2, RectTrm.rect.height / 2);
        position = new Vector2(Mathf.Clamp(position.x, 0, RectTrm.rect.width), Mathf.Clamp(position.y, 0, RectTrm.rect.height));

        return position;
    }


    public void ResetTexture()
    {
        Graphics.Blit(_mainTex, _rt);
        _image.texture = _rt;
    }


    /// <summary> 렌더 텍스쳐에 브러시 텍스쳐로 그리기 </summary>
    public void DrawTexture(in Vector2 uv)
    {
        RenderTexture.active = _rt; // 페인팅을 위해 활성 렌더 텍스쳐 임시 할당
        GL.PushMatrix();                                  // 매트릭스 백업
        GL.LoadPixelMatrix(0, resolution, resolution, 0); // 알맞은 크기로 픽셀 매트릭스 설정

        float brushPixelSize = brushSize * resolution;

        // 렌더 텍스쳐에 브러시 텍스쳐를 이용해 그리기
        Graphics.DrawTexture(
            new Rect(
                uv.x - brushPixelSize * 0.5f,
                (_rt.height - uv.y) - brushPixelSize * 0.5f,
                brushPixelSize,
                brushPixelSize
            ),
            brushTexture
        );

        GL.PopMatrix();              // 매트릭스 복구
        RenderTexture.active = null; // 활성 렌더 텍스쳐 해제
    }
}
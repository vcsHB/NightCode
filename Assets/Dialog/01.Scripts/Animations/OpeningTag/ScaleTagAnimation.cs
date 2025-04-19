
using UnityEditor.Rendering;
using UnityEngine;

namespace Dialog
{
    public class ScaleTagAnimation : TagAnimation
    {
        private float[] timer;
        private float _duration = 0.1f;
        private float _amplitude = -1f;

        public ScaleTagAnimation()
        {
            _timing = AnimTiming.Start;
            tagType = TagEnum.Scale;
            _checkEndPos = true;
        }

        public override void Init()
        {
            base.Init();
            timer = new float[animLength];

            for (int i = 0; i < animLength; i++)
                timer[i] = 0;
        }

        public override void Complete()
        {

        }

        public override bool SetParameter()
        {
            return true;
        }

        public override void Play()
        {
            for (int i = 0; i < animLength; ++i)
            {
                var charInfo = _txtInfo.characterInfo[animStartPos + i];
                if (!charInfo.isVisible) continue;


                Vector3[] verts = _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    Vector3 middlePos = (verts[charInfo.vertexIndex + 0] + verts[charInfo.vertexIndex + 2]) / 2f;
                    verts[charInfo.vertexIndex + j] = Vector3.LerpUnclamped(verts[charInfo.vertexIndex + j], middlePos,
                        Mathf.Lerp(_amplitude, 0, timer[i]));
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;

                timer[i] += Time.deltaTime / _duration;
            }
        }
    }
}

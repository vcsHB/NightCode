
using UnityEngine;

namespace Dialog.Animation
{
    public class ScaleTagAnimation : TagAnimation
    {
        private float[] timer;
        private float _duration = 1;
        private float _amplitude = 2;

        public override void OnStartTag()
        {
            this.timer = new float[animLength];
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
                    Vector3 middlePos = (verts[0] + verts[2]) / 2;
                    Vector3 current = verts[i];

                    verts[j] =
                        Vector3.LerpUnclamped(current,
                    middlePos,
                        Mathf.Pow((1 - (timer[i] / _duration)), 2) * _amplitude);
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;
                timer[i] += Time.deltaTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class FromupTagAnimation : TagAnimation
    {
        private float[] progress;
        private float maxPos = 50;

        public FromupTagAnimation()
        {
            _timing = AnimTiming.Start;
            tagType = TagEnum.Fromup;
            _checkEndPos = true;
        }

        public override void Play()
        {
            for (int i = 0; i < animLength; ++i)
            {
                var charInfo = _txtInfo.characterInfo[animStartPos + i];
                if (!charInfo.isVisible) continue;
                if (progress[i] <= 0) continue;


                Vector3[] verts = _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    var orig = verts[charInfo.vertexIndex + j];

                    float y = progress[i] * maxPos;
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, y, 0);
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;
                progress[i] -= Time.deltaTime * 4;
            }
        }

        public override void Complete()
        {

        }

        public override void Init()
        {
            base.Init();
            progress = new float[animLength];

            for (int i = 0; i < animLength; i++)
                progress[i] = 1;
        }

        public override bool SetParameter()
        {
            return true;
        }
    }
}

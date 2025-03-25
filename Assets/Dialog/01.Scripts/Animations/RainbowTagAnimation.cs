using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class RainbowTagAnimation : TagAnimation
    {
        private Gradient _rainbow = new Gradient();

        public RainbowTagAnimation()
        {
            _timing = AnimTiming.Update;
            tagType = TagEnum.Rainbow;
            _checkEndPos = true;

            //무지개당ㅎㅎ
            _rainbow.mode = GradientMode.Blend;
            _rainbow.colorKeys = new GradientColorKey[]
                {
                new GradientColorKey(new Color(255,0,0), 0f),
                new GradientColorKey(new Color(255,0,255), 0.15f),
                new GradientColorKey(new Color(0,0,255), 0.333f),
                new GradientColorKey(new Color(0,255,0), 0.666f),
                new GradientColorKey(new Color(225,255,0), 0.85f),
                new GradientColorKey(new Color(255,0,0), 1f),
                };
            _rainbow.alphaKeys = new GradientAlphaKey[]
                {
                new GradientAlphaKey(1,0),
                new GradientAlphaKey(1,1),
                };
        }

        public override void Play()
        {
            for (int i = 0; i < animLength; ++i)
            {
                var charInfo = _txtInfo.characterInfo[animStartPos + i];
                var meshInfo = _txtInfo.meshInfo[charInfo.materialReferenceIndex];

                if (!charInfo.isVisible) continue;

                int index = charInfo.vertexIndex;

                var vertices = meshInfo.vertices;
                var colors = meshInfo.colors32;

                for (int j = 0; j < 4; ++j)
                {
                    colors[index + j] = _rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + j].x * 0.001f, 1f));
                    colors[index + j].a = meshInfo.colors32[index + j].a;
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].colors32 = colors;
            }
        }

        public override void Complete()
        {

        }

        public override bool SetParameter() { return true; }
    }
}

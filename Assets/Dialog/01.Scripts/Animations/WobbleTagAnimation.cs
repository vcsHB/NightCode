using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class WobbleTagAnimation : TagAnimation
    {
        private float _power;

        public WobbleTagAnimation()
        {
            _timing = AnimTiming.Update;
            tagType = TagEnum.Wobble;
            _checkEndPos = true;
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
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * _power, 0);
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;
            }
        }

        public override void Complete()
        {

        }

        public override bool SetParameter()
        {
            if (float.TryParse(Param, out _power) == false)
            {
                Debug.LogError($"{tagType.ToString()} ({Param}) : Parameter is wrong");
                return false;
            }
            return true;
        }
    }
}

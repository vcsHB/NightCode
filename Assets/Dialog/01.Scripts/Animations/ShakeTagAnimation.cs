using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class ShakeTagAnimation : TagAnimation
    {
        private float _power;

        public ShakeTagAnimation()
        {
            _timing = AnimTiming.Update;
            tagType = TagEnum.Shake;
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

                    float x = Mathf.Sin((Time.time + i) * 62.8f) * _power;
                    float y = Mathf.Cos((Time.time + i) * 40f) * _power;
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(x, y, 0);
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

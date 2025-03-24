using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class TodownTagAnimation : TagAnimation
    {
        private float _speed;
        private float _velocity;

        public TodownTagAnimation()
        {
            _timing = AnimTiming.End;
            tagType = TagEnum.Todown;
            _checkEndPos = true;
        }

        public override void Play()
        {
            if(_velocity <= -1000)
            {
                _endAnimating = true;
                return;
            }

            for (int i = 0; i < animLength; ++i)
            {
                var charInfo = _txtInfo.characterInfo[animStartPos + i];

                if (!charInfo.isVisible) continue;

                Vector3[] verts = _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    var orig = verts[charInfo.vertexIndex + j];

                    _velocity -= _speed * Time.deltaTime;
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, _velocity, 0);
                }

                _txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;
            }
        }

        public override void Complete()
        {

        }

        public override void Init()
        {
            base.Init();
            _velocity = 0;
        }

        public override bool SetParameter()
        {
            if (float.TryParse(Param, out _speed) == false)
            {
                Debug.LogError($"{tagType.ToString()} ({Param}) : Parameter is wrong");
                return false;
            }
            return true;
        }

        public override void SetTextInfo(TMP_TextInfo txtInfo)
        {
            base.SetTextInfo(txtInfo);
            _endAnimating = false;
        }

    }
}

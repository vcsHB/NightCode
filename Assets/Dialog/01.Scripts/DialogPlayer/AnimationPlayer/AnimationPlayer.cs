using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class AnimationPlayer : MonoBehaviour
    {
        private DialogPlayer _player;
        private TextMeshProUGUI _txt;

        private void Awake()
        {
            _player = GetComponent<DialogPlayer>();
        }

        public void PlayAnimation(TextMeshProUGUI tmp, List<TagAnimation> animList)
        {
            tmp.ForceMeshUpdate();
            TMP_TextInfo txtInfo = tmp.textInfo;

            bool playedEndAnim = false;

            animList.ForEach(anim =>
            {
                if (anim.Timing == AnimTiming.End)
                {
                    if (_player.PlayingEndAnimation && !anim.EndAnimating)
                    {
                        anim.SetTextInfo(txtInfo);
                        anim.Play();
                        playedEndAnim = true;
                    }
                    return;
                }

                anim.SetTextInfo(txtInfo);
                anim.Play();
            });

            if (!playedEndAnim) _player.CompleteEndAnimation();

            for (int i = 0; i < txtInfo.meshInfo.Length; ++i)
            {
                var meshInfo = txtInfo.meshInfo[i];

                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;

                tmp.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }
}

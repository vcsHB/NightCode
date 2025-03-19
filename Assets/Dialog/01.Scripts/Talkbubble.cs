using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Dialog
{
    public class Talkbubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;
        private NodeSO _node;
        private DialogPlayer _dialogPlayer;

        private void LateUpdate()
        {
            if (_node == null || _node is not NormalNodeSO normalNode) return;

            _tmp.ForceMeshUpdate();
            TMP_TextInfo txtInfo = _tmp.textInfo;

            bool playedEndAnim = false;

            normalNode.contentTagAnimations.ForEach(anim =>
            {
                if (anim.Timing == AnimTiming.End)
                {
                    if (_dialogPlayer.PlayingEndAnimation && !anim.EndAnimating)
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

            if (!playedEndAnim) _dialogPlayer.CompleteEndAnimation();

            for (int i = 0; i < txtInfo.meshInfo.Length; ++i)
            {
                var meshInfo = txtInfo.meshInfo[i];

                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;

                _tmp.UpdateGeometry(meshInfo.mesh, i);
            }
        }

        public void SetTalkbubble(NodeSO node)
        {
            if (node is NormalNodeSO normalNode)
            {
                _node = node;
                _tmp.SetText(normalNode.GetContents());
                gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        public void TurnOffTalkbubble()
        {
            gameObject.SetActive(false);
            _node = null;
            _tmp.SetText("");
        }

        public void Init(DialogPlayer player)
        {
            _dialogPlayer = player;
        }
    }
}

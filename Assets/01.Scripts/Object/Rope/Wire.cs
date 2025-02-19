using Agents.Players;
using UnityEngine;
namespace ObjectManage.Rope
{

    public class Wire : MonoBehaviour
    {
        [SerializeField] private AimAnchor _anchor;
        [SerializeField] private float _wireLengthThreshold = 8f;
        [SerializeField] private float _pullLength = 1f;
        private RopePhysics _ropePhysics;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _ropePhysics = GetComponent<RopePhysics>();
            _lineRenderer = GetComponent<LineRenderer>();
        }
        

        public void SetWireEnable(bool enable)
        {
            _ropePhysics.UpdateSegment();
            _lineRenderer.enabled = enable;
            _ropePhysics.Enabled = enable;
            _anchor.SetJointEnable(enable);
        }

        public void SetWireEnable(bool enable, Vector2 targetPos, float length)
        {
            SetWireEnable(enable);
            _anchor.SetPos(targetPos);
           
            _anchor.SetLength(length);
        }

    }
}
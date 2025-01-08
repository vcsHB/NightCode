using UnityEngine;
namespace Agents.Players
{

   

    public class AimAnchor : MonoBehaviour
    {
        [SerializeField] private Sprite[] _aimMarkSprites;
        private SpriteRenderer _aimRenderer;
        private DistanceJoint2D _jointCompo;
        private Transform _visualTrm;

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _aimRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _jointCompo = GetComponent<DistanceJoint2D>();
        }

        public void SetJointEnable(bool value)
        {
            _jointCompo.enabled = value;
        }

        public void SetLength(float distance)
        {
            _jointCompo.distance = distance;
        }
        public void SetPos(Vector2 position)
        {
            transform.position = position;

        }
    }
}
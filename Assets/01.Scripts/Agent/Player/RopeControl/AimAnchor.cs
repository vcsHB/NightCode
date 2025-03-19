using ObjectManage.OtherObjects;
using UnityEngine;
namespace Agents.Players
{
    public class AimAnchor : MonoBehaviour
    {
        [SerializeField] private Sprite[] _aimMarkSprites;
        [SerializeField] private ParticleSystem _anchorVFX;
        [SerializeField] private InstantFlashLight _light;
        private SpriteRenderer _aimRenderer;
        [SerializeField] private GameObject _anchorPhysicObject;
        private DistanceJoint2D _jointCompo;
        private Transform _visualTrm;

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _aimRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _jointCompo = _anchorPhysicObject.GetComponent<DistanceJoint2D>();
        }

        public void SetJointEnable(bool value)
        {
            _jointCompo.enabled = value;
            _anchorPhysicObject.SetActive(value);
        }

        public void SetLength(float distance)
        {
            _jointCompo.distance = distance;
        }
        public void SetPos(Vector2 position)
        {
            transform.position = position;
            _anchorPhysicObject.transform.localPosition = Vector2.zero;
            _anchorVFX.Play();
            _light.Play();

        }

        public void SetOwnerPlayerRigidbody(Rigidbody2D owner)
        {
            _jointCompo.connectedBody = owner;
        }
    }
}
using System;
using Agents.Players;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.Rope
{
    public class AimGroupController : MonoBehaviour
    {
        public UnityEvent OnAnchorLocatedEvent;
        [SerializeField] private Transform _virtualAimTrm;
        [SerializeField] private Transform _aimMarkTrm;
        [SerializeField] private Transform _anchorTrm;
        [SerializeField] private AimAnchor _aimAnchor;
        [field: SerializeField] public Wire Wire { get; private set; }
        [field: SerializeField] public RopePhysics RopePhysics { get; private set; }
        // Properties
        public Vector2 AnchorPos => _anchorTrm.position;
        public Vector2 VirtualAimPos => _virtualAimTrm.position;
        [SerializeField] private Transform _swingOrbitVisualTrm;
        private SpriteRenderer _swingOrbitVisualRenderer;
        private Material _swingOrbitMaterial;
        private readonly int _orbitPlayerPositionHash = Shader.PropertyToID("_PlayerPos");

        private void Awake()
        {
            _swingOrbitVisualRenderer = _swingOrbitVisualTrm.GetComponent<SpriteRenderer>();
            _swingOrbitMaterial = _swingOrbitVisualRenderer.material;
        }

        public void SetVirtualAimPosition(Vector2 position)
        {
            _virtualAimTrm.position = position;
        }

        public void SetVirtualAim(bool value)
        {
            _virtualAimTrm.gameObject.SetActive(value);
        }


        public void SetAimMarkPosition(Vector2 position)
        {
            _aimMarkTrm.position = position;
        }

        public void SetAnchorPosition(Vector2 position)
        {
            _anchorTrm.position = position;
        }
        public void SetAnchorParent(Transform parent = null)
        {
            if (parent == null)
                parent = transform;
            _anchorTrm.SetParent(parent);
        }
        public void SetActiveWire(bool value)
        {
            if (value)
                OnAnchorLocatedEvent?.Invoke();

            Wire.gameObject.SetActive(value);
        }

        public void SetAnchorOwner(Rigidbody2D ownerRigid, Transform ropeHolder)
        {
            _aimAnchor.SetOwnerPlayerRigidbody(ownerRigid);
            RopePhysics.startTransform = ropeHolder;

        }

        public void SetOrbitVisual(bool value)
        {
            _swingOrbitVisualRenderer.enabled = value;
        }

        public void SetOrbitVisual(Vector2 position)
        {
            _swingOrbitMaterial.SetVector(_orbitPlayerPositionHash, position);
        }

        public void SetAimColor(Color personalColor)
        {
            _aimAnchor.SetColor(personalColor);
        }
        public void SetWireEnable(bool enable)
        {
            Wire.SetWireEnable(enable);
        }

        public void SetWireEnable(bool enable, Vector2 targetPos, float length)
        {
            Wire.SetWireEnable(enable, targetPos, length);
            _swingOrbitVisualTrm.localScale = Vector3.one * length * 2f;
        }
    }
}
using Agents.Players;
using UnityEngine;

namespace ObjectManage.Rope
{
    public class AimGroupController : MonoBehaviour
    {
        [SerializeField] private Transform _virtualAimTrm;
        [SerializeField] private Transform _aimMarkTrm;
        [SerializeField] private Transform _anchorTrm;
        [SerializeField] private AimAnchor _aimAnchor;
        [field: SerializeField] public Wire Wire { get; private set; }
        [field: SerializeField] public RopePhysics RopePhysics { get; private set; }
        // Properties
        public Vector2 AnchorPos => _anchorTrm.position;
        public Vector2 VirtualAimPos => _virtualAimTrm.position;

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
            print("앵커위치는 지금부터 " + position + "입니다");
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
            Wire.gameObject.SetActive(value);
        }

        public void SetAnchorOwner(Rigidbody2D ownerRigid, Transform ropeHolder)
        {
            _aimAnchor.SetOwnerPlayerRigidbody(ownerRigid);
            RopePhysics.startTransform = ropeHolder;

        }
    }
}
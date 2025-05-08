using System;
using Combat;
using UnityEngine;
namespace ObjectManage
{

    public class AimMagnetPoint : MonoBehaviour, IGrabable
    {
        public Transform GetTransform => transform;
        //private SpriteRenderer _aimedMarkRenderer;
        private Health _ownerHealth;
        private Collider2D _collider;
        private AimMagnetPointVisual _aimMagnetVisual;
        private bool _isVisualExist;
        private void Awake()
        {
            _ownerHealth = GetComponentInParent<Health>();
            _collider = GetComponent<Collider2D>();
            _aimMagnetVisual = GetComponentInChildren<AimMagnetPointVisual>();
            _isVisualExist = _aimMagnetVisual != null;
            if (_ownerHealth != null)
                _ownerHealth.OnDieEvent.AddListener(HandleOwnerDie);
        }


        private void HandleOwnerDie()
        {
            SetEnabled(false);

        }

        public void Grab()
        {
            if (_isVisualExist)
                _aimMagnetVisual.SetEnabled(true);
        }

        public void Release()
        {
            if (_isVisualExist)
                _aimMagnetVisual.SetEnabled(false);
        }

        private void SetEnabled(bool value)
        {
            _collider.enabled = value;
            if (!value)
                Release();
        }

        public void OnAimEntered()
        {
            //_aimedMarkRenderer.enabled = true;
        }

        public void OnAimExited()
        {
            //_aimedMarkRenderer.enabled = false;

        }
    }
}
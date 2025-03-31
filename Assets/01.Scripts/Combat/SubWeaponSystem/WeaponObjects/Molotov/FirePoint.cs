using Combat.Casters;
using UnityEngine;

namespace Combat.SubWeaponSystem
{
    public class FirePoint : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private Caster _caster;
        [SerializeField] private ParticleSystem _vfxPlayer;
        private float _fireStartTime;
        private bool _isActive;

        [ContextMenu("DebugSetFire")]
        private void DebugSetFireActive()
        {
            SetFire(true);
        }


        public void SetFire(bool value)
        {
            _fireStartTime = Time.time;
            _isActive = value;
            if (value)
                _vfxPlayer.Play();
            else
                _vfxPlayer.Stop();

        }

        private void Update()
        {
            if (!_isActive) return;
            if (_fireStartTime + _lifeTime < Time.time)
            {
                SetFire(false);
            }
            _caster.Cast();
        }


    }

}
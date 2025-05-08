using Combat.Casters;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.BossManage
{
    public class BurnOutLaser : MonoBehaviour
    {
        public UnityEvent OnFireEvent;
        public UnityEvent OnFireStopEvent;
        [SerializeField] private float _laserWidth;
        [SerializeField] private float _fireMaxDistance = 20f;
        [SerializeField] private LayerMask _laserIgnoreLayer;
        [SerializeField] private float _laserSizeTweenDuration = 0.2f;
        [SerializeField] private ParticleSystem _fireVFX;
        [SerializeField] private Transform _visualTrm;
        private CircleRayCaster _caster;
        private bool _isFire;

        private void Awake()
        {
            _caster = GetComponent<CircleRayCaster>();
        }

        public void StartFire()
        {
            _fireVFX.Play();
            OnFireEvent?.Invoke();
            _visualTrm.DOScaleX(_laserWidth, _laserSizeTweenDuration);
            _isFire = true;
        }
        private void Update()
        {
            //DetectObstacle();
            if (_isFire)
            {
                _caster.SetDirection(transform.up);
                _caster.Cast();
            }
        }

        public void StopFire()
        {
            _fireVFX.Stop();
            OnFireStopEvent?.Invoke();
            _visualTrm.DOScaleX(0f, _laserSizeTweenDuration);
            _isFire = false;
        }

    }
}
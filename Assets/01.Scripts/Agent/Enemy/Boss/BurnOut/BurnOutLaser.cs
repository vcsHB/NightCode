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

        public void StartFire()
        {
            _fireVFX.Play();
            OnFireEvent?.Invoke();
            _visualTrm.DOScaleX(_laserWidth, _laserSizeTweenDuration);
        }
        private void Update()
        {
            DetectObstacle();
        }

        public void StopFire()
        {
            _fireVFX.Stop();
            OnFireStopEvent?.Invoke();
            _visualTrm.DOScaleX(0f, _laserSizeTweenDuration);

        }

        private void DetectObstacle()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _fireMaxDistance, _laserIgnoreLayer);
            if (hit.collider != null)
            {

            }
        }
    }
}
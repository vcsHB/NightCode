using System;
using System.Collections;
using Combat;
using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players
{
    public class GrabController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private ThrowDirectionMark _throwDirectionMark;
        private bool _isComboComplete;
        private Player _player;

        [Header("Pull Setting")]
        [SerializeField] private float _pullDuration = 0.3f;

        [Header("Throw Setting")]
        [SerializeField] private float _throwPower;
        [SerializeField] private float _throwSuccessDamage;
        private Caster _throwCaster; // 넉백 캐스터 만들기, 넉백중 벽과충돌시 

        private GrabData _currentGrabData;
        private AimData _currentAimData;


        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            AimDetector aimDetector = _player.GetCompo<AimDetector>();
            aimDetector.OnAimEvent += HandleRefreshAim;
            aimDetector.OnGrabEvent += HandleRefreshGrab;
        }

        public void SetCompleteCombo()
        {
            _isComboComplete = true;
            _throwDirectionMark.SetTargetMark(true);
        }

        public void ThrowTarget()
        {
            if (_isComboComplete)
            {
                // 에임 방향으로 던지기
            }
            else
            {
                // 포물선 위로 던지기
            }
            _isComboComplete = false;
            _throwDirectionMark.SetTargetMark(false);
        }

        public void PullTarget()
        {
            // 당기기
            if (!_currentGrabData.isTargeted) return;

            StartCoroutine(PullCoroutine());

        }
        private IEnumerator PullCoroutine()
        {
            Transform targetTrm = _currentGrabData.grabTarget.GetTransform;
            float currentTime = 0f;
            
            Vector2 previousPosition = targetTrm.position;
            Vector2 targetPosition = Vector2.Lerp(previousPosition, _player.transform.position, 0.9f);
            float duration = _pullDuration * _currentAimData.distance;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / duration;
                targetTrm.position = Vector2.Lerp(previousPosition, targetPosition, ratio);
                yield return null;
            }


        }






        private void HandleRefreshAim(AimData data)
        {
            _currentAimData = data;
            _throwDirectionMark.SetDirection(data.aimDirection);
        }

        private void HandleRefreshGrab(GrabData data)
        {
            _currentGrabData = data;
        }
    }


}
using System;
using System.Collections;
using Combat;
using ObjectManage;
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
        [SerializeField] private SlashVFXPlayer _strongThrowImpact;
        [SerializeField] private float _throwPower;
        [SerializeField] private float _throwSuccessDamage;
        [SerializeField] private Caster _throwCaster; // 넉백 캐스터 만들기, 넉백중 벽과충돌시 
        public bool IsPulled { get; private set; }
        private GrabData _currentGrabData;
        private AimData _currentAimData;

        private AgentRenderer _agentRenderer;
        private Transform _grabTargetTrm;


        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _agentRenderer = _player.GetCompo<AgentRenderer>();
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
            _isComboComplete = true;
            if (_isComboComplete)
            {
                // 에임 방향으로 던지기
                _currentAimData.aimDirection.Normalize();
                Vector2 direction = _currentAimData.aimDirection * _throwPower * 1.5f;
                _strongThrowImpact.SetDirection(_currentAimData.aimDirection);
                _strongThrowImpact.Play();
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("StrongThrow"));
                _throwCaster.SendCasterData(new KnockbackCasterData(direction, 10f, true, _throwSuccessDamage));
            }
            else
            {
                // 포물선 위로 던지기
                Vector2 direction = new Vector2(_agentRenderer.FacingDirection * _throwPower, 6f);
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("WeakThrow"));
                _throwCaster.SendCasterData(new KnockbackCasterData(direction, 10f));

            }
            _throwCaster.ForceCast(_grabTargetTrm.GetComponent<Collider2D>());
            _isComboComplete = false;
            IsPulled = false;
            _throwDirectionMark.SetTargetMark(false);
        }



        public void PullTarget(Action OnComplete)
        {
            // 당기기
            if (!_currentGrabData.isTargeted) return;

            StartCoroutine(PullCoroutine(OnComplete));

        }
        private IEnumerator PullCoroutine(Action OnComplete)
        {
            _grabTargetTrm = _currentGrabData.grabTarget.GetTransform;
            float currentTime = 0f;

            Vector2 previousPosition = _grabTargetTrm.position;
            Vector2 targetPosition = Vector2.Lerp(previousPosition, _player.transform.position, 0.9f);
            float duration = _pullDuration * _currentAimData.distance;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / duration;
                _grabTargetTrm.position = Vector2.Lerp(previousPosition, targetPosition, ratio);
                yield return null;
            }
            IsPulled = true;
            OnComplete?.Invoke();

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
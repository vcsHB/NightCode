using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.BossManage
{

    public class BurnOutBossMovement : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnMovementStartEvent;
        public UnityEvent OnMovementEndEvent;
        private BurnOutBoss _boss;
        [SerializeField] private float _axisMovementMaxDuration = 10f;
        [SerializeField] private float _horizontalMovementSpeed = 3f;
        [SerializeField] private float _verticalMovementSpeed = 2f;
        private float _axisMovementDefaultDuration;

        [SerializeField] private Transform _verticalRail; // X Movement Follow
        [SerializeField] private Transform _horizontalRail; // Y Movement Follow
        private Vector2 _targetPosition;
        [SerializeField] private Transform _targetPosTrm;
        private BossPatternPositionController _patternPositionController;
        public Vector2 BossPosition => _boss.transform.position;


        private void Awake()
        {
            _axisMovementDefaultDuration = _axisMovementMaxDuration;
        }

        public void SetAxisDuration(float newDuration)
        {
            _axisMovementMaxDuration = newDuration;
        }

        public void ResetAxisDuration()
        {
            _axisMovementMaxDuration = _axisMovementDefaultDuration;
        }
        public void Initialize(Agent agent)
        {
            _boss = agent as BurnOutBoss;
            _patternPositionController = _boss.GetComponent<BossPatternPositionController>();
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void MoveToStatePosition(BurnOutStateEnum state, Action OnArriveEvent = null)
        {
            SetMovement(_patternPositionController.GetStateSequencePosition(state), OnArriveEvent);
        }

        public void SetMovement(Vector2 targetPosition, Action OnMovementCompleteEvent)
        {
            _targetPosition = targetPosition;
            StartCoroutine(MoveToTarget(OnMovementCompleteEvent));

        }


        private IEnumerator MoveToTarget(Action OnMovementCompleteEvent)
        {
            OnMovementEndEvent?.Invoke();
            yield return StartCoroutine(MoveYAxis()); // Y Axis Move

            yield return StartCoroutine(MoveXAxis()); // X Axis Move
            // Arrive
            transform.position = _targetPosition;
            OnMovementStartEvent?.Invoke();
            OnMovementCompleteEvent?.Invoke();
        }

        private IEnumerator MoveYAxis()
        {
            float fixedX = transform.position.x;

            yield return MoveAxis(
                _horizontalRail,
                pos => pos.y,
                (pos, value) => { pos.y = value; return pos; },
                _targetPosition.y,
                _verticalMovementSpeed,
                (pos, value) => { pos.x = value; return pos; },
                fixedX
            );
        }

        private IEnumerator MoveXAxis()
        {
            float fixedY = transform.position.y;

            yield return MoveAxis(
                _verticalRail,
                pos => pos.x,
                (pos, value) => { pos.x = value; return pos; },
                _targetPosition.x,
                _horizontalMovementSpeed,
                (pos, value) => { pos.y = value; return pos; },
                fixedY
            );
        }
        private IEnumerator MoveAxis(
        Transform railTransform,
        Func<Vector2, float> getAxis,
        Func<Vector2, float, Vector2> setAxis,
        float targetValue,
        float speed,
        Func<Vector2, float, Vector2> setFixedAxis,
        float fixedAxisValue
        )
        {
            Vector2 currentPos = transform.position;
            Vector2 railPos = railTransform.position;

            while (Mathf.Abs(targetValue - getAxis(currentPos)) > 0.01f)
            {
                float currentValue = getAxis(currentPos);
                float direction = Mathf.Sign(targetValue - currentValue);
                float move = direction * speed * Time.deltaTime;
                float newAxisValue = currentValue + move;

                if (Mathf.Sign(targetValue - currentValue) != Mathf.Sign(targetValue - newAxisValue))
                {
                    newAxisValue = targetValue;
                }

                currentPos = setAxis(currentPos, newAxisValue);
                currentPos = setFixedAxis(currentPos, fixedAxisValue);

                transform.position = currentPos;

                railPos = setAxis(railPos, newAxisValue);
                railTransform.position = railPos;

                yield return null;
            }
        }


    }
}
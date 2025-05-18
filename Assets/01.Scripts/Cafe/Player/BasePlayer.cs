using Base.Cafe;
using System;
using UnityEngine;

namespace Base
{
    public class AvatarPlayer : AvatarEntity
    {
        public BaseInput input;
        [SerializeField] private BaseTalkBubble _talkBubble;
        [SerializeField] private BasePlayerInteractMark _interactMark;
        [SerializeField] private ProcessInputObject _clickProcess;

        public bool isGetFood { get => food != null; }
        public FoodSO food { get; private set; }

        private void OnEnable()
        {
            input.EnableInput();
        }

        #region Food

        public void SetFood(FoodSO food)
        {
            this.food = food;
            _talkBubble.Open();
            _talkBubble.SetIcon(food.icon);
            //���� ������ ��� �ִٴ� ǥ��?
        }

        public void ServeFood()
        {
            food = null;

            Flip();
            _talkBubble.Close();
            stateMachine.ChangeState("Idle");
        }

        #endregion


        #region Interact


        public void AddInteract(Action interact)
        {
            if (food != null) _talkBubble.Close();

            _interactMark.onInteract += interact;
            _interactMark.gameObject.SetActive(true);
        }

        public void RemoveInteract(Action interact)
        {
            if (food != null) _talkBubble.Open();

            _interactMark.onInteract -= interact;
            _interactMark.gameObject.SetActive(false);
        }

        public void AddClickProcessInteract(Action interact)
        {
            _clickProcess.Init();
            _clickProcess.OnComplete += interact;
        }

        public void RemoveClickProcessInteract(Action interact)
        {
            _clickProcess.Close();
            _clickProcess.OnComplete -= interact;
        }

        #endregion


        public override void SetMoveTarget(Transform target)
        {
            input.DisableInput();
            base.SetMoveTarget(target);
            stateMachine.ChangeState("MoveToTarget");
        }
    }
}

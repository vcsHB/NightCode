using Basement;
using System;
using UnityEngine;

namespace Cafe
{
    public class CafePlayer : CafeEntity
    {
        public CafeInput input;
        public TalkBubble talkBubble;
        [SerializeField] private CafePlayerInteractMark _interactMark;
        [SerializeField] private ProcessInputObject _clickProcess;

        public bool isGetFood { get => food != null; }
        public FoodSO food { get; private set; }


        #region Food

        public void SetFood(FoodSO food)
        {
            this.food = food;
            talkBubble.Open();
            talkBubble.SetIcon(food.icon);
            //뭔가 음식을 들고 있다는 표현?
        }

        public void ServeFood()
        {
            food = null;

            Flip();
            talkBubble.Close();
            stateMachine.ChangeState("Idle");
        }

        #endregion


        #region Interact


        public void AddInteract(Action interact)
        {
            if (food != null) talkBubble.Close();

            _interactMark.onInteract += interact;
            _interactMark.gameObject.SetActive(true);
        }

        public void RemoveInteract(Action interact)
        {
            if (food != null) talkBubble.Open();

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

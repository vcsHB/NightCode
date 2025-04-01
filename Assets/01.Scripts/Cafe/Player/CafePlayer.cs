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
            talkBubble.Close();
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
            Debug.Log(food);
            if (food != null) talkBubble.Open();

            _interactMark.onInteract -= interact;
            _interactMark.gameObject.SetActive(false);
        }


        #endregion
    }
}

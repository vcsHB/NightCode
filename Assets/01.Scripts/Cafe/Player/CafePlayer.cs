using Basement;
using System;
using UnityEngine;

namespace Cafe
{
    public class CafePlayer : CafeEntity
    {
        public event Action onInteract;

        public CafeInput input;
        public TalkBubble talkBubble;
        private CafePlayerInputObject _inputObject;

        public bool isGetFood { get => food != null; }
        public FoodSO food { get; private set; }


        private void OnEnable()
        {
            input.onInteract += OnInteract;
        }

        private void OnDisable()
        {
            input.onInteract -= OnInteract;
        }


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

        private void OnInteract()
        {
            onInteract?.Invoke();
            onInteract = null;
            //interactMark.SetActive(false);
        }

        public void AddInteract(CafePlayerInputObject inputObject)
        {
            _inputObject = Instantiate(inputObject, transform);
            _inputObject.Open();
            //interactMark.SetActive(true);
        }

        public void RemoveInteract()
        {
            _inputObject.Close();
        }


        #endregion
    }
}

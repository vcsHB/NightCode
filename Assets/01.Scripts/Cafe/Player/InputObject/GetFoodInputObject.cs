using UnityEngine;

namespace Cafe
{
    public class GetFoodInputObject : CafePlayerInputObject
    {
        //Debuging
        public CafePlayer player;
        public Kitchen kitchen;

        private void OnEnable()
        {
            input.onInteract += GetFood;
        }
        private void OnDisable()
        {
            input.onInteract -= GetFood;
        }


        public void GetFood()
        {
            FoodSO food = kitchen.GetFood();
            if (food == null) return;

            player.SetFood(food);
            Close();
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }
        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

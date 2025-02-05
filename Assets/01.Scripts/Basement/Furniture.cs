using System;
using UnityEngine;
using Basement.Player;

namespace Basement
{
    public class Furniture : MonoBehaviour
    {
        public Action InteractAction;
        private BasementPlayer _player;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            _player.SetInteractAction(InteractAction);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            _player.RemoveInteractAction(InteractAction);
        }

        public void Init(BasementPlayer player) => _player = player;
    }
}


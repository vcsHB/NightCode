using System.Collections.Generic;
using UnityEngine;

namespace Basement.NPC
{
    public abstract class NPC : MonoBehaviour
    {
        [SerializeField]private List<NPCState> _stateList;
        private NPCState _currentState;

        public NPCState CurrentState => _currentState;

        public void ChangeState(string state)
        {

        }
    }
}

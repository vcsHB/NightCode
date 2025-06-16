using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class ChipsetFunction : MonoBehaviour
    {
        protected Player _owner;
        public virtual void Initialize(Player owner)
        {
            _owner = owner;

        }

        
    }
}
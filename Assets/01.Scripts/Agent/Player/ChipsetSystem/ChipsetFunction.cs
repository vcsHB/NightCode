using Combat.PlayerTagSystem;
using StatSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class ChipsetFunction : MonoBehaviour
    {
        protected Player _owner;
        protected AgentStatus _status;
        protected EnvironmentData _environmentData;

        public virtual void Initialize(Player owner, EnvironmentData enviromentData)
        {
            _owner = owner;
            _environmentData = enviromentData;
            _status = _owner.GetCompo<AgentStatus>();

        }
    }
}
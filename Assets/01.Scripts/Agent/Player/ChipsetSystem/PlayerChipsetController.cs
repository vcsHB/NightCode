using System.Collections.Generic;
using Chipset;
using Combat.PlayerTagSystem;
using Dialog.Animation;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class PlayerChipsetController : MonoBehaviour, IAgentComponent
    {
        private List<ChipsetFunction> _chipsets = new();
        private EnvironmentData _environmentData;

        private Player _owner;
        public void AddChipsetFunction(ChipsetSO data)
        {
            ChipsetFunction function = Instantiate(data.chipsetFunctionPrefab, transform);
            function.Initialize(_owner, _environmentData);
            _chipsets.Add(function);

        }

        public void Initialize(EnvironmentData environmentData)
        {
            _environmentData = environmentData;
        }

        public void Initialize(Agent agent)
        {
            _owner = agent as Player;

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

    }
}
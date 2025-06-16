using System.Collections.Generic;
using Chipset;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class PlayerChipsetController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private ChipsetGruopSO _chipsetGroupData;
        private Dictionary<ushort, ChipsetFunction> _chipsetFunctions = new();


        public void SetChipsetData(ushort[] chipsetId)
        {
            for (int i = 0; i < chipsetId.Length; i++)
            {
                ChipsetSO data = _chipsetGroupData.GetChipset(chipsetId[i]);
                ChipsetFunction function = Instantiate(data.chipsetFunctionPrefab, transform);
                _chipsetFunctions.Add(chipsetId[i], function);

            }
        }

        public void Initialize(Agent agent)
        {

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

    }
}
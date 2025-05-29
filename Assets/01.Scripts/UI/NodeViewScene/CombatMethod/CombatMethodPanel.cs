using System.Collections.Generic;
using Agents.Players.WeaponSystem;
using UnityEngine;

namespace UI.NodeViewScene
{

    public class CombatMethodPanel : MonoBehaviour
    {
        [SerializeField] private int _initializePoolAmount = 5;
        [SerializeField] private CombatMethodSlot _combatMethodSlotPrefab;
        [SerializeField] private Transform _contentTrm;

        private Queue<CombatMethodSlot> _slotPool;
        private List<CombatMethodSlot> _enabledSlotList = new();


        private void Awake()
        {
            GeneratePool();
        }

        public void SetCombatMethodTagData(CombatMethodTagSO[] tagDatas)
        {
            for (int i = 0; i < tagDatas.Length; i++)
            {
                CombatMethodSlot slot = GetNewSlot();
                slot.SetCombatMethodData(tagDatas[i]);
            }
        }

        public void GeneratePool()
        {
            for (int i = 0; i < _initializePoolAmount; i++)
            {
                CombatMethodSlot newSlot = Instantiate(_combatMethodSlotPrefab, _contentTrm);
                _slotPool.Enqueue(newSlot);
            }
        }
        public CombatMethodSlot GetNewSlot()
        {
            CombatMethodSlot newSlot = _slotPool.Count > 0 ?
                _slotPool.Dequeue() :
                Instantiate(_combatMethodSlotPrefab, _contentTrm);
            _enabledSlotList.Add(newSlot);
            return newSlot;
        }
    }

}
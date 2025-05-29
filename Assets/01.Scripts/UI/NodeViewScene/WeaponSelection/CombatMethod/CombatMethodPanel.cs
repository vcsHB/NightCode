using System.Collections.Generic;
using Agents.Players.WeaponSystem;
using UnityEngine;

namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class CombatMethodPanel : MonoBehaviour
    {
        [SerializeField] private int _initializePoolAmount = 5;
        [SerializeField] private CombatMethodSlot _combatMethodSlotPrefab;
        [SerializeField] private Transform _contentTrm;

        private Queue<CombatMethodSlot> _slotPool = new();
        private List<CombatMethodSlot> _enabledSlotList = new();


        private void Awake()
        {
            GeneratePool();
        }

        public void SetCombatMethodTagData(CombatMethodTagSO[] tagDatas)
        {
            for (int i = 0; i < _enabledSlotList.Count; i++)
            {
                _enabledSlotList[i].SetActive(false);
            }
            _enabledSlotList.Clear();
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
                newSlot.SetActive(false);
                _slotPool.Enqueue(newSlot);
            }
        }
        public CombatMethodSlot GetNewSlot()
        {
            CombatMethodSlot newSlot = _slotPool.Count > 0 ?
                _slotPool.Dequeue() :
                Instantiate(_combatMethodSlotPrefab, _contentTrm);
            _enabledSlotList.Add(newSlot);
            newSlot.SetActive(true);
            return newSlot;
        }
    }

}
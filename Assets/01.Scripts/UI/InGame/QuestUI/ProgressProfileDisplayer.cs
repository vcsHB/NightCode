using System.Collections.Generic;
using System.Data.Common;
using QuestSystem;
using QuestSystem.QuestTarget;
using UnityEngine;
namespace UI.InGame.GameUI.QuestSyetem
{

    public class ProgressProfileDisplayer : ProgressDisplayer
    {
        [SerializeField] private ProgressProfile _profilePrefab;
        [SerializeField] private RectTransform _contentTrm;

        private List<ProgressProfile> _enabledProfiles = new();
        private Queue<ProgressProfile> _profileSlotPool = new();


        private void Awake()
        {
            _contentTrm = transform as RectTransform;
        }

        public override void SetQuestData(QuestSO data)
        {
            base.SetQuestData(data);
            SetAllDisable();
            foreach (TargetInfoSO info in data.targetInfoList)
            {

                ProgressProfile profile = _profileSlotPool.Count > 0
                       ? _profileSlotPool.Dequeue()
                       : Instantiate(_profilePrefab, _contentTrm);

                profile.SetActive(true);
                profile.Initialize(info);

                _enabledProfiles.Add(profile);
            }
        }

        private void SetAllDisable()
        {
            foreach (var profile in _enabledProfiles)
            {
                profile.gameObject.SetActive(false);
                _profileSlotPool.Enqueue(profile); // 풀에 반환
            }
            _enabledProfiles.Clear();
        }

        public override void SetProgress(QuestData data)
        {
            for (int i = 0; i < data.clearList.Length; i++)
            {
                _enabledProfiles[i].SetComplete(data.clearList[i]);
            }

        }
    }
}
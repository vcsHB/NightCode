using Agents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Basement.Training
{
    public class TrainingManager : MonoSingleton<TrainingManager>
    {
        [SerializeField] private Time startTime;
        [SerializeField] private Time endTime;

        private string _path = Path.Combine(Application.dataPath, "Training.json");

        private Dictionary<CharacterEnum, int> _fatigues;
        private Dictionary<CharacterEnum, SkillPoint> _skillPoints;
        private Time currentTime;

        private Dictionary<CharacterEnum, TrainingInfo> characterTrainingInfo;
        public Time CurrentTime => currentTime;

        public bool TryGetTrainingInfo(CharacterEnum character, out TrainingInfo info)
            => characterTrainingInfo.TryGetValue(character, out info);


        protected override void Awake()
        {
            base.Awake();
            characterTrainingInfo = new Dictionary<CharacterEnum, TrainingInfo>();
            Load();

            currentTime = startTime;
        }

        public void AddMinute(int minute)
        {
            currentTime.AddMinute(minute);

            Debug.Log(currentTime.hour);
            if(currentTime.hour >= endTime.hour)
            {
                BasementManager.Instance.basement.CompleteScadule();
                return;
            }
            
            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (characterTrainingInfo.TryGetValue(character, out TrainingInfo info))
                {
                    info.remainTime -= minute;
                    characterTrainingInfo[character] = info;

                    if (info.remainTime <= 0)
                        CompleteTraining(character);
                }
            }
        }

        public void AddCharacterTraining(CharacterEnum character, TrainingSO training)
        {
            TrainingInfo trainingInfo = new TrainingInfo();
            trainingInfo.training = training.GetInstance();
            trainingInfo.remainTime = training.requireTime;
            trainingInfo.startTime = currentTime;
            trainingInfo.isStartTraining = false;

            characterTrainingInfo.Add(character, trainingInfo);
        }

        public void CancelTraining(CharacterEnum character)
        {
            characterTrainingInfo.Remove(character);
        }

        public void CompleteTraining(CharacterEnum character)
        {
            TrainingInfo info = characterTrainingInfo[character];

            TrainingResult result = info.training.GetResult(character);
            int increaseValue = info.training.increaseValue[result];
            int increaseFatigue = info.training.requireFatigue;

            AddFatigue(character, increaseFatigue);
            AddSkillPoint(character, info.training.statType, increaseValue);

            string trainingCompleteText = $"{info.training.trainingVisibleName} Complete\n피로도 +{increaseFatigue}    {info.training.statType.ToString()} pt +{increaseValue}";
            UIManager.Instance.msgText.PopMSGText(character, trainingCompleteText);

            CancelTraining(character);
        }

        #region ValueGetSet

        public void AddFatigue(CharacterEnum character, int value)
        {
            //뭐 더해졌을 때 빼졌을 때 효과같은거 추가 할 수도 있음
            _fatigues[character] += value;
        }

        public int GetFatigue(CharacterEnum character)
            => _fatigues[character];

        public void UseSkillPoint(CharacterEnum character, SkillPointEnum skillPointType, int value)
            => _skillPoints[character].AddSkillPoint(skillPointType, -value);

        public void AddSkillPoint(CharacterEnum character, SkillPointEnum skillPointType, int value)
            => _skillPoints[character].AddSkillPoint(skillPointType, value);

        public int GetSkillPoint(CharacterEnum character, SkillPointEnum skillPointType)
            => _skillPoints[character].GetSkillPoint(skillPointType);

        #endregion

        #region Save&Load

        public void Save()
        {
            TrainingSave save = new TrainingSave();

            save.fatigue = _fatigues.Values.ToList();
            save.skillPoints = _skillPoints.Values.ToList();

            string json = JsonUtility.ToJson(save);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            _fatigues = new Dictionary<CharacterEnum, int>();
            _skillPoints = new Dictionary<CharacterEnum, SkillPoint>();

            if (File.Exists(_path) == false)
            {
                foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
                {
                    _fatigues.Add(character, 0);
                    _skillPoints.Add(character, new SkillPoint());
                }

                Save();
                return;
            }

            string json = File.ReadAllText(_path);
            TrainingSave save = JsonUtility.FromJson<TrainingSave>(json);

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                _fatigues.Add(character, save.fatigue[(int)character]);
                _skillPoints.Add(character, save.skillPoints[(int)character]);
            }
        }

        #endregion
    }

    public struct TrainingInfo
    {
        public TrainingSO training;
        public Time startTime;
        public int remainTime;

        public bool isStartTraining;

    }

    public enum SkillPointEnum
    {
        Health,
        Intelligence,
        Dexdexterity
    }

    [Serializable]
    public class Time
    {
        public int hour;
        public int minute;

        public Time(int h, int m)
        {
            hour = h;
            minute = m;
        }

        public void AddMinute(int time)
        {
            minute += time;
            hour += minute / 60;
            minute %= 60;
        }
    }

    [Serializable]
    public class SkillPoint
    {
        public int Health;
        public int Intelligence;
        public int Dexdexterity;

        public int GetSkillPoint(SkillPointEnum skillPointType)
        {
            switch (skillPointType)
            {
                case SkillPointEnum.Health:
                    return Health;
                case SkillPointEnum.Intelligence:
                    return Intelligence;
                case SkillPointEnum.Dexdexterity:
                    return Dexdexterity;
                default:
                    return default;
            }
        }

        public void AddSkillPoint(SkillPointEnum skillPointType, int value)
        {
            switch (skillPointType)
            {
                case SkillPointEnum.Health:
                    Health += value;
                    break;
                case SkillPointEnum.Intelligence:
                    Intelligence += value;
                    break;
                case SkillPointEnum.Dexdexterity:
                    Dexdexterity += value;
                    break;
            }
        }
    }

    [Serializable]
    public class TrainingSave
    {
        public List<int> fatigue = new();
        public List<SkillPoint> skillPoints = new();
    }
}

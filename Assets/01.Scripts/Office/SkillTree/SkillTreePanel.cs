using DG.Tweening;
using UnityEngine;
using Office.CharacterSkillTree;
using System.Collections.Generic;
using System.IO;
using System;

namespace Office
{
    public class SkillTreePanel : OfficeUIParent
    {
        [Space]
        public SkillTree[] skillTrees;
        [SerializeField] private float _easingDuration;

        private Tween _openCloseTween;
        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        private string _path => Path.Combine(Application.dataPath, "TechTree.json");

        private void Awake()
        {
            RectTrm.anchoredPosition = new Vector2(0, -screenPosition.y);
        }

        private void Start()
        {
            Load();
        }


        public void Save()
        {
            StatSave statSave = new StatSave();
            for (int i = 0; i < 3; i++)
            {
                statSave.treeSave.Add(skillTrees[i].GetTreeSave());
            }

            string json = JsonUtility.ToJson(statSave);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                Save();
                return;
            }

            string json = File.ReadAllText(_path);
            List<TechTreeSave> treeSave = JsonUtility.FromJson<List<TechTreeSave>>(json);

            for (int i = 0; i < treeSave.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (skillTrees[j].characterType == treeSave[i].characterType)
                        skillTrees[j].Load(treeSave[i]);
                }
            }
        }

        #region UI

        public void UpdateSkillTree(CharacterEnum characterType)
        {
            for (int i = 0; i < skillTrees.Length; i++)
            {
                if (i == (int)characterType)
                {
                    skillTrees[i].Open();
                }
                else
                {
                    skillTrees[i].Close();
                }
            }
        }

        public void InitSkillTree(CharacterEnum characterType)
        {
            for (int i = 0; i < skillTrees.Length; i++)
            {
                skillTrees[i].Init();
                if (i == (int)characterType)
                {
                    skillTrees[i].Open();
                }
                else
                {
                    skillTrees[i].Close();
                }
            }
        }

        public override void OpenAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(0, _easingDuration)
                .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(-screenPosition.y, _easingDuration)
                .OnComplete(OnCompleteClose);
        }
        #endregion
    }

    [Serializable]
    public class StatSave
    {
        public List<TechTreeSave> treeSave = new List<TechTreeSave>();
    }
}


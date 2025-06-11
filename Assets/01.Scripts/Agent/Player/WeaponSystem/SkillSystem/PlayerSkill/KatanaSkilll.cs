using System.Collections;
using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.SkillSystem
{

    public class KatanaSkilll : PlayerSkill
    {
        private Caster _caster;

        [SerializeField] private int _maxTargetAmount = 5;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _attackTerm = 0.1f;
        private WaitForSeconds _waitForTerm;
        [SerializeField] private Transform _slashEffectTrm;
        private TrailRenderer _slashEffectRenderer;
        private void Awake()
        {
            _caster = GetComponentInChildren<Caster>();
            _slashEffectRenderer = _slashEffectTrm.GetComponent<TrailRenderer>();
            _waitForTerm = new WaitForSeconds(_attackTerm);
        }

        protected override void UseSkill()
        {
            StartCoroutine(SkillCoroutine());
        }

        private IEnumerator SkillCoroutine()
        {
            Collider2D[] targets;
            _targetDetector.DetectTargetsSorted(out targets);
            if (targets.Length <= 0) yield break;
            Time.timeScale = 0.5f; // must be refactor by TimeMamager
            _slashEffectRenderer.Clear();
            _slashEffectRenderer.emitting = true;
            for (int i = 0; i < targets.Length; i++)
            {
                if (i == _maxTargetAmount) break;

                Collider2D target = targets[i];
                _slashEffectTrm.position = target.transform.position;
                _caster.ForceCast(target);
                yield return _waitForTerm;
            }
            Time.timeScale = 1f;
            _slashEffectRenderer.emitting = false;
        }
    }
}
using System;
using UnityEngine;
namespace Combat.SubWeaponSystem
{
    public class GrenadeBomb : Grenade
    {
        private Transform _visualTrm;
        private Material _visualMaterial;
        private readonly int _heatLevelHash = Shader.PropertyToID("_HeatLevel");

        protected override void Awake()
        {
            base.Awake();
            _visualTrm = _visualRenderer.transform;
            _visualMaterial = _visualRenderer.material;
            OnDelayTimeEvent += HandleSetGrenadeHeatLevel;
        }
        public override void ResetObject()
        {
            base.ResetObject();
            HandleSetGrenadeHeatLevel(0f);
        }



        private void HandleSetGrenadeHeatLevel(float current, float max)
        {
            HandleSetGrenadeHeatLevel(current / max);
        }

        private void HandleSetGrenadeHeatLevel(float ratio)
        {
            _visualMaterial.SetFloat(_heatLevelHash, ratio);

        }

        

        public override void Explode()
        {
            _caster.Cast();


        }
    }
}
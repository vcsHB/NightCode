using System;
using Combat.Casters;
using Combat.PlayerTagSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class FirePoint : ChipsetFunction
    {
        [SerializeField] private Caster _caster;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            owner.OnEnterEvent += HandlePlayerEnter;
        }

        private void HandlePlayerEnter()
        {
            _caster.Cast();
        }
    }
}
namespace Agents.Players
{
    using UnityEngine;

    public class CresentPlayerRenderer : PlayerRenderer
    {
        [SerializeField] private CresentPlayerSwingTrajectoryVisual _swingAttackDirectionVisual;

        public void SetSwingAttackDirectionVisualEnable(bool value)
        {
            _swingAttackDirectionVisual.SetVisualEnable(value);
        }
    }
}
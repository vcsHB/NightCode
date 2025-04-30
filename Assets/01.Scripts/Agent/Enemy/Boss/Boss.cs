namespace Agents.Enemies.BossManage
{

    public class Boss : Enemy
    {
        public BossPhaseController PhaseController {get; protected set;}

        protected override void Awake()
        {
            base.Awake();
            PhaseController = GetComponent<BossPhaseController>();
        }
    }

}
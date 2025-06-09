using Agents;

namespace EffectSystem
{
	public class EffectStun : EffectState
	{
		public EffectStun(Agent agent, bool isResist) : base(agent, isResist)
		{
		}

		public override void Over()
		{
			base.Over();
		}

		public override void Start(int stack = 1, int level = 1, float percent = 1f)
		{
			base.Start(level, stack);
		}

		public override void UpdateBySecond()
		{
			base.UpdateBySecond();
		}
	}
}
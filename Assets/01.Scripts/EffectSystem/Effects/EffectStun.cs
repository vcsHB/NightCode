using Agents;

namespace EffectSystem
{
	public class EffectStun : EffectState
	{
		public override void Over()
		{
			base.Over();
		}

		public override void Apply(int stack = 1, int level = 1, float percent = 1f)
		{
			base.Apply(level, stack);
		}

		public override void UpdateBySecond()
		{
			base.UpdateBySecond();
		}

		public override void SetEffectType()
		{
			EffectType = EffectStateTypeEnum.Repair;
		}
	}
}
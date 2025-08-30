public class CollectXPCard : ActiveCard
{
	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			PlayerExperience player = playerObj.GetComponentInChildren<PlayerExperience>();
			if (player != null)
			{
				ExperienceGem[] xpOrbs = FindObjectsOfType<ExperienceGem>();
				foreach (ExperienceGem exp in xpOrbs)
					exp.Collect(player);
				TriggerCooldown();
			}
		}
	}
}
using System.Collections;

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
				StartCoroutine(CollectGemCoroutine(player, xpOrbs));
				TriggerCooldown();
			}
		}
	}

	IEnumerator CollectGemCoroutine(PlayerExperience player, ExperienceGem[] xpOrbs)
	{
		foreach (ExperienceGem exp in xpOrbs)
		{
			exp.Collect(player);
			yield return null;
		}
	}
}
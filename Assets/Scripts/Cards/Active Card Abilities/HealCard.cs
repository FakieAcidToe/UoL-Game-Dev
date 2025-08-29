using UnityEngine;

public class HealCard : ActiveCard
{
	[Header("Heal Card Properties")]
	[SerializeField, Min(0)] int[] healAmount;
	[SerializeField, TextArea] string[] upgradeBlurbs;

	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			playerObj.GetComponent<PlayerHP>().TakeDamage(-GetCurrentHealAmount());
			TriggerCooldown();
		}
	}

	int GetCurrentHealAmount()
	{
		return healAmount[Mathf.Clamp(GetDupeTimes() - 1, 0, healAmount.Length - 1)];
	}

	public override int GetMaxDupeTimes()
	{
		return healAmount.Length;
	}

	public override string GetBlurb()
	{
		int i = GetDupeTimes();
		if (i >= 0 && i < upgradeBlurbs.Length)
			return upgradeBlurbs[i];
		return "";
	}
}
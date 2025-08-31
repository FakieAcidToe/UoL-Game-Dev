using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class SingleProjectileSpawnerCard : ActiveCard
{
	[SerializeField] Hitbox[] hitboxPrefab;
	[SerializeField, TextArea] string[] upgradeBlurbs;
	ProjectileSpawner spawner;

	protected override void Start()
	{
		spawner = GetComponent<ProjectileSpawner>();

		base.Start();
	}

	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			spawner.SpawnHitbox(playerObj, GetCurrentHitbox());
			TriggerCooldown();
		}
	}

	Hitbox GetCurrentHitbox()
	{
		return hitboxPrefab[Mathf.Clamp(GetDupeTimes() - 1, 0, hitboxPrefab.Length - 1)];
	}

	public override int GetMaxDupeTimes()
	{
		return hitboxPrefab.Length;
	}

	public override string GetBlurb()
	{
		int i = GetDupeTimes();
		if (i >= 0 && i < hitboxPrefab.Length)
			return upgradeBlurbs[i];
		return "";
	}
}
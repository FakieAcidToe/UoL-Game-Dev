using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class SingleProjectileSpawnerCard : ActiveCard
{
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
			spawner.SpawnHitbox(playerObj);
			TriggerCooldown();
		}
	}
}
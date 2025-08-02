using UnityEngine;

public class TurretCard : ActiveCard
{
	[Header("Turret Card Properties")]
	[SerializeField] ProjectileSpawner turretPrefab;
	ProjectileSpawner spawnedTurret;

	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			if (spawnedTurret != null) Destroy(spawnedTurret.gameObject);
			spawnedTurret = Instantiate(turretPrefab, playerObj.position, Quaternion.identity);
			TriggerCooldown();
		}
	}
}
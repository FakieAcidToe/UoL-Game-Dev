using UnityEngine;

public class TurretCard : ActiveCard
{
	[Header("Turret Card Properties")]
	[SerializeField] ProjectileSpawner[] turretPrefab;
	[SerializeField, TextArea] string[] upgradeBlurbs;
	ProjectileSpawner spawnedTurret;

	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			if (spawnedTurret != null) Destroy(spawnedTurret.gameObject);
			spawnedTurret = Instantiate(GetCurrentPrefab(), playerObj.position, Quaternion.identity);
			TriggerCooldown();
		}
	}

	public override void DestroySelf()
	{
		if (spawnedTurret != null)
			Destroy(spawnedTurret.gameObject);

		base.DestroySelf();
	}

	ProjectileSpawner GetCurrentPrefab()
	{
		return turretPrefab[Mathf.Clamp(GetDupeTimes() - 1, 0, turretPrefab.Length - 1)];
	}

	public override int GetMaxDupeTimes()
	{
		return turretPrefab.Length;
	}

	public override string GetBlurb()
	{
		int i = GetDupeTimes();
		if (i >= 0 && i < upgradeBlurbs.Length)
			return upgradeBlurbs[i];
		return "";
	}
}
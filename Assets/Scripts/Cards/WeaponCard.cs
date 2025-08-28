using UnityEngine;

public class WeaponCard : AbstractCard
{
	[SerializeField] ProjectileSpawner[] weaponPrefabs;

	ProjectileSpawner spawnedWeapon;

	public override void OnPickup(int dupeTimes)
	{
		base.OnPickup(dupeTimes);
		if (dupeTimes > 0)
		{
			ProjectileSpawner weaponPrefab = weaponPrefabs[Mathf.Min(dupeTimes, weaponPrefabs.Length) - 1];
			spawnedWeapon = Instantiate(weaponPrefab, playerObj);
		}
	}

	public override void DestroySelf()
	{
		if (spawnedWeapon != null)
			Destroy(spawnedWeapon.gameObject);

		base.DestroySelf();
	}

	public override int GetMaxDupeTimes()
	{
		return weaponPrefabs.Length;
	}
}
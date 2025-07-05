using UnityEngine;

public class WeaponCard : AbstractCard
{
	[SerializeField] ProjectileSpawner weaponPrefab;

	ProjectileSpawner spawnedWeapon;

	protected override void OnPickup()
	{
		spawnedWeapon = Instantiate(weaponPrefab, playerObj);
	}
}
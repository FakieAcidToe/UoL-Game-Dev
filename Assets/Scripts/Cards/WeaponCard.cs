using UnityEngine;

public class WeaponCard : AbstractCard
{
	[SerializeField] ProjectileSpawner weaponPrefab;

	ProjectileSpawner spawnedWeapon;

	public override void OnPickup()
	{
		spawnedWeapon = Instantiate(weaponPrefab, playerObj);
	}
}
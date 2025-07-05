using UnityEngine;

public class WeaponCard : AbstractCard
{
	[SerializeField] ProjectileSpawner weaponPrefab;

	protected override void OnPickup()
	{
		Debug.Log("Give player weapon");
		// give playerobj the weapon
	}
}
using UnityEngine;

[CreateAssetMenu(fileName = "Starting Equipment", menuName = "ScriptableObjects/Starting Equipment Data")]
public class PlayerStats : ScriptableObject
{
	[Header("Player Sprites")]
	public PlayerAnimationSet animationSet;

	[Header("Starting Weapon Card")]
	public WeaponCard weaponCard;

	[Header("Stats")]
	public int maxHP = 100;
}
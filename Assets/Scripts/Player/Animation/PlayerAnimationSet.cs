using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationSet", menuName = "ScriptableObjects/PlayerAnimationSet")]
public class PlayerAnimationSet : ScriptableObject
{
	[Header("Healthbar Portrait")]
	public Sprite portrait;

	[Header("Idle")]
	public Sprite idleSide1;
	public Sprite idleSide2;

	public Sprite idleFront1;
	public Sprite idleFront2;

	public Sprite idleBack1;
	public Sprite idleBack2;

	[Header("Walk")]
	public Sprite walkSide1;
	public Sprite walkSide2;

	public Sprite walkFront1;
	public Sprite walkFront2;

	public Sprite walkBack1;
	public Sprite walkBack2;
}
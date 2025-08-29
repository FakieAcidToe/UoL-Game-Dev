using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class TurretSpritePlayer : MonoBehaviour
{
	[SerializeField] Sprite[] sprites;
	[SerializeField] SpriteRenderer spriteRenderer;

	ProjectileSpawner spawner;
	Vector2 lastDirection = Vector2.zero;

	void Awake()
	{
		spawner = GetComponent<ProjectileSpawner>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 dir = spawner.GetHitboxDirectionThisTick();
		if (dir != Vector2.zero)
		{
			lastDirection = dir;
			spriteRenderer.sprite = sprites[DirectionToIndex(lastDirection)];
		}
	}

	int DirectionToIndex(Vector2 dir)
	{
		if (dir == Vector2.zero) return -1;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle = (angle + 360) % 360;
		int index = Mathf.RoundToInt(angle / 45f) % 8;
		return index;
	}
}
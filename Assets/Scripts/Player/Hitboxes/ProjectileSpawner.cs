using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[System.Serializable]
	struct ProjectileSettings
	{
		[Range(0, 1)] public float timeOffset;
		[Range(-180, 180)] public float angleOffset; // should call ReCalculateAngleOffset if changed

		[HideInInspector] public float timer;
		[HideInInspector] public Vector2 angle; // is handled by ReCalculateAngleOffset from angleOffset
	}

	[SerializeField] Hitbox projectilePrefab;
	[SerializeField] bool requireMouseDirection = true;
	[Tooltip("Time in seconds between each bullet"), SerializeField, Min(0)] float interval;
	[Tooltip("Amount to displace bullet in its movement direction on spawn"), SerializeField, Min(0)] float positionOffset;
	[Tooltip("False: Spawn hitbox at the same level as this object\nTrue: Spawn hitbox as a child of this object"), SerializeField] bool spawnAsChild = true;
	[Tooltip("Rotate hitbox sprite to set direction?"), SerializeField] bool rotateHitboxSprite = true;
	[SerializeField] ProjectileSettings[] projectileSettings;

	Camera mainCamera;

	void Awake()
	{
		mainCamera = Camera.main;

		ResetWeaponTiming();

		ReCalculateAngleOffset();

	}

	void Update()
	{
		float dt = Time.deltaTime;
		for (int i = 0; i < projectileSettings.Length; ++i)
		{
			projectileSettings[i].timer += dt;

			if (projectileSettings[i].timer >= interval)
			{
				SpawnHitbox(projectileSettings[i].angle);
				projectileSettings[i].timer -= interval;
			}
		}
	}

	public Hitbox SpawnHitbox(Vector2 _offset)
	{
		Vector2 direction = Vector2.zero;
		Quaternion rotation = Quaternion.identity;

		// set direction
		if (requireMouseDirection)
		{
			direction = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
			if (_offset != Vector2.right)
				direction = new Vector2(direction.x * _offset.x - direction.y * _offset.y, direction.x * _offset.y + direction.y * _offset.x);
		}
		else
		{
			// attempt to inherit direction from gameobject
			Hitbox hitbox = GetComponent<Hitbox>();
			if (hitbox != null)
				direction = hitbox.GetDirection();
		}

		// rotate hitbox sprite
		if (rotateHitboxSprite && direction != Vector2.zero)
			rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

		Hitbox newHitbox = Instantiate(projectilePrefab, transform.position, rotation, spawnAsChild ? transform : transform.parent);
		newHitbox.SetDirection(direction);

		if (positionOffset > 0)
			newHitbox.transform.position += (Vector3)(positionOffset * newHitbox.GetDirection());

		return newHitbox;
	}

	public Hitbox SpawnHitbox()
	{
		return SpawnHitbox(Vector2.right);
	}

	public void ResetWeaponTiming()
	{
		for (int i = 0; i < projectileSettings.Length; ++i)
			projectileSettings[i].timer = (1f - projectileSettings[i].timeOffset) * interval;
	}

	public void ReCalculateAngleOffset()
	{
		for (int i = 0; i < projectileSettings.Length; ++i)
			projectileSettings[i].angle = new Vector2(Mathf.Cos(projectileSettings[i].angleOffset * Mathf.Deg2Rad), Mathf.Sin(projectileSettings[i].angleOffset * Mathf.Deg2Rad));
	}
}
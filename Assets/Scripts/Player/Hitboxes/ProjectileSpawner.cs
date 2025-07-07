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
	//[Tooltip("True: Projectile that moves relative to world\nFalse: Melee/Physical Hitbox that follows the weapon's position"), SerializeField] bool spawnInWorldSpace = true;
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
		Hitbox newHitbox = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);

		if (requireMouseDirection)
		{
			Vector2 v = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
			if (_offset != Vector2.right)
				v = new Vector2(v.x * _offset.x - v.y * _offset.y, v.x * _offset.y + v.y * _offset.x);
			newHitbox.SetDirection(v);
		}

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
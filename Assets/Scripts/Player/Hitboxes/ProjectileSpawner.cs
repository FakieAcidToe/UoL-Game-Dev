using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	enum DirectionProperty
	{
		ObjectToMouseDirection,
		InheritFromSelfHitbox,
		Vector2Right,
		Random,
		NearestEnemy,
		NoDirection
	}

	[System.Serializable]
	struct ProjectileSettings
	{
		[Range(0, 1)] public float timeOffset;
		[Range(-180, 180)] public float angleOffset; // should call ReCalculateAngleOffset if changed

		[HideInInspector] public float timer;
		[HideInInspector] public Vector2 angle; // is handled by ReCalculateAngleOffset from angleOffset
	}

	[SerializeField] Hitbox projectilePrefab;
	[SerializeField] DirectionProperty directionType = DirectionProperty.ObjectToMouseDirection;
	[Tooltip("Time in seconds between each bullet"), SerializeField, Min(0)] float interval;
	[Tooltip("Amount to displace bullet in its movement direction on spawn"), SerializeField, Min(0)] float positionOffset;
	[Tooltip("False: Spawn hitbox at the same level as this object\nTrue: Spawn hitbox as a child of this object"), SerializeField] bool spawnAsChild = true;
	[Tooltip("Rotate hitbox sprite to set direction?"), SerializeField] bool rotateHitboxSprite = true;
	[SerializeField] ProjectileSettings[] projectileSettings;

	Camera mainCamera;
	Vector2 hitboxDirectionThisTick;

	void Awake()
	{
		mainCamera = Camera.main;

		ResetWeaponTiming();

		ReCalculateAngleOffset();

	}

	void Update()
	{
		float dt = Time.deltaTime;
		hitboxDirectionThisTick = Vector2.zero;
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

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		ReCalculateAngleOffset();
		float length = 10f;
		for (int i = 0; i < projectileSettings.Length; ++i)
		{
			Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + projectileSettings[i].angle * length);
			CircleGizmo.DrawCircle((Vector2)transform.position + projectileSettings[i].angle * length * (1 - projectileSettings[i].timeOffset), 0.2f, 8);
		}
	}

	Vector2 GetDirectionToClosestEnemy()
	{
		EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
		Transform closest = null;
		float minDistance = Mathf.Infinity;

		foreach (EnemyMovement enemy in enemies)
		{
			float distance = Vector2.Distance(transform.position, enemy.transform.position);
			if (distance < minDistance)
			{
				minDistance = distance;
				closest = enemy.transform;
			}
		}

		if (closest != null)
		{
			Vector2 dir = (closest.position - transform.position).normalized;
			return dir;
		}

		return Vector2.zero; // no enemy found
	}

	public Hitbox SpawnHitbox(Vector2 _offset, Transform _hitboxSource = null)
	{
		Quaternion rotation = Quaternion.identity;
		Transform source = _hitboxSource == null ? transform : _hitboxSource;

		// set direction
		if (hitboxDirectionThisTick == Vector2.zero)
			switch (directionType)
			{
				case DirectionProperty.ObjectToMouseDirection:
					hitboxDirectionThisTick = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - source.position).normalized;
					break;
				case DirectionProperty.InheritFromSelfHitbox:
					// attempt to inherit direction from gameobject
					Hitbox hitbox = GetComponent<Hitbox>();
					if (hitbox != null)
						hitboxDirectionThisTick = hitbox.GetDirection();
					break;
				case DirectionProperty.Vector2Right:
					hitboxDirectionThisTick = Vector2.right;
					break;
				case DirectionProperty.Random:
					hitboxDirectionThisTick = Random.insideUnitCircle.normalized;
					break;
				case DirectionProperty.NearestEnemy:
					hitboxDirectionThisTick = GetDirectionToClosestEnemy();
					if (hitboxDirectionThisTick == Vector2.zero) goto case DirectionProperty.Random;
					break;
				default:
				case DirectionProperty.NoDirection:
					break;
			}

		Vector2 direction = hitboxDirectionThisTick;

		// adjust direction based on offset
		if (_offset != Vector2.right)
			direction = new Vector2(direction.x * _offset.x - direction.y * _offset.y, direction.x * _offset.y + direction.y * _offset.x);

		// rotate hitbox sprite
		if (rotateHitboxSprite && direction != Vector2.zero)
			rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

		Hitbox newHitbox = Instantiate(projectilePrefab, source.position, rotation, spawnAsChild ? source : source.parent);
		newHitbox.SetDirection(direction);

		if (positionOffset > 0)
			newHitbox.transform.position += (Vector3)(positionOffset * newHitbox.GetDirection());

		return newHitbox;
	}

	public Hitbox SpawnHitbox(Transform _hitboxSource = null)
	{
		return SpawnHitbox(Vector2.right, _hitboxSource);
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
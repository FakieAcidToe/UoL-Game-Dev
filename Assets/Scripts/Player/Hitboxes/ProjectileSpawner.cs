using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[System.Serializable]
	struct ProjectileSettings
	{
		[Range(0, 1)] public float timeOffset;
		[Range(-180, 180)] public float angleOffset; // should call ReCalculateAngleOffset if changed
		[Tooltip("0 = Infinity"), Min(0)] public uint spawnNumberOfTimes;

		[HideInInspector] public float timer;
		[HideInInspector] public Vector2 angle; // is handled by ReCalculateAngleOffset from angleOffset
		[HideInInspector] public uint spawnedAmount;
	}

	[SerializeField] Hitbox projectilePrefab;
	[SerializeField] DirectionProperties.DirectionProperty directionType = DirectionProperties.DirectionProperty.ObjectToMouseDirection;
	[Tooltip("Time in seconds between each bullet"), SerializeField, Min(0)] float interval;
	[SerializeField] bool affectedByAttackSpeedUpgrade = true;
	[Tooltip("Amount to displace bullet in its movement direction on spawn"), SerializeField, Min(0)] float positionOffset;
	[Tooltip("False: Spawn hitbox at the same level as this object\nTrue: Spawn hitbox as a child of this object"), SerializeField] bool spawnAsChild = true;
	[Tooltip("Rotate hitbox sprite to set direction?"), SerializeField] bool rotateHitboxSprite = true;
	[SerializeField] ProjectileSettings[] projectileSettings;
	[SerializeField] AudioClip sfx;

	List<Hitbox> limitedHitboxes; // hitboxes that are spawned with a spawn limit

	Camera mainCamera;
	Vector2 hitboxDirectionThisTick;

	void Awake()
	{
		mainCamera = Camera.main;

		if (affectedByAttackSpeedUpgrade && PlayerStatus.Instance != null && PlayerStatus.Instance.playerATKSPD > 0)
			interval /= PlayerStatus.Instance.playerATKSPD;

		ResetWeaponTiming();

		ReCalculateAngleOffset();

		// only instantiate limitedHitboxes if theres a need to
		foreach (ProjectileSettings setting in projectileSettings)
			if (setting.spawnNumberOfTimes > 0)
			{
				limitedHitboxes = new List<Hitbox>();
				break;
			}
	}

	void Update()
	{
		if (Time.timeScale <= 0) return;

		float dt = Time.deltaTime;
		hitboxDirectionThisTick = Vector2.zero;
		bool hasSFXThisFrame = false;
		for (int i = 0; i < projectileSettings.Length; ++i)
		{
			projectileSettings[i].timer += dt;

			if (projectileSettings[i].timer >= interval &&
				(projectileSettings[i].spawnNumberOfTimes == 0 || projectileSettings[i].spawnedAmount < projectileSettings[i].spawnNumberOfTimes))
			{
				if (sfx != null && !hasSFXThisFrame)
				{
					hasSFXThisFrame = true;
					SoundManager.Instance.Play(sfx);
				}
				Hitbox hbox = SpawnHitbox(projectileSettings[i].angle);
				projectileSettings[i].timer -= interval;

				if (projectileSettings[i].spawnNumberOfTimes > 0)
				{
					++projectileSettings[i].spawnedAmount;
					limitedHitboxes.Add(hbox);
				}
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

	public Hitbox SpawnHitbox(Vector2 _offset, Transform _hitboxSource = null)
	{
		Quaternion rotation = Quaternion.identity;
		Transform source = _hitboxSource == null ? transform : _hitboxSource;

		// set direction
		if (hitboxDirectionThisTick == Vector2.zero)
			hitboxDirectionThisTick = DirectionProperties.GetDirectionFromProperty(directionType, source, gameObject);

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
		{
			projectileSettings[i].timer = (1f - projectileSettings[i].timeOffset) * interval;
			projectileSettings[i].spawnedAmount = 0;
		}
	}

	public void ReCalculateAngleOffset()
	{
		for (int i = 0; i < projectileSettings.Length; ++i)
			projectileSettings[i].angle = new Vector2(Mathf.Cos(projectileSettings[i].angleOffset * Mathf.Deg2Rad), Mathf.Sin(projectileSettings[i].angleOffset * Mathf.Deg2Rad));
	}

	void OnDestroy()
	{
		if (limitedHitboxes != null)
			foreach (Hitbox hbox in limitedHitboxes)
				if (hbox != null && hbox.gameObject != null)
					Destroy(hbox.gameObject);
	}

	public Vector2 GetHitboxDirectionThisTick()
	{
		return hitboxDirectionThisTick;
	}
}
using System.Collections.Generic;
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

	[SerializeField] Projectile projectilePrefab;
	[SerializeField] bool requireMouseDirection = true;
	[Tooltip("Time in seconds between each bullet"), SerializeField, Min(0)] float interval;
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
				SpawnProjectile(projectileSettings[i].angle);
				projectileSettings[i].timer -= interval;
			}
		}
	}

	public Projectile SpawnProjectile(Vector2 _offset)
	{
		Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		if (requireMouseDirection)
		{
			Vector2 v = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
			if (_offset != Vector2.right)
				v = new Vector2(v.x * _offset.x - v.y * _offset.y, v.x * _offset.y + v.y * _offset.x);
			newProjectile.SetDirection(v);
		}

		return newProjectile;
	}

	public Projectile SpawnProjectile()
	{
		return SpawnProjectile(Vector2.right);
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
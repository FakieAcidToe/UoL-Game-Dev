using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField] Projectile projectilePrefab;
	[SerializeField] float interval = 2f; // Time in seconds between each bullet

	float timer = 0f;

	Camera mainCamera;
	
	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= interval)
		{
			SpawnProjectile();
			timer = 0f; // reset timer
		}
	}

	public Projectile SpawnProjectile()
	{
		Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		// mouse direction from gameobject
		newProjectile.SetDirection((mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);

		return newProjectile;
	}
}
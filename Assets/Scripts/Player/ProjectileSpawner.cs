using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField] Projectile projectilePrefab;
	[SerializeField] float interval = 2f; // Time in seconds between each bullet

	float timer = 0f;
	public Vector2 mouseDirection { get; private set; }

	Camera mainCamera;
	
	void Start()
	{
		mainCamera = Camera.main;
		mouseDirection = Vector2.zero;
	}

	void Update()
	{
		timer += Time.deltaTime;

		mouseDirection = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

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
		newProjectile.SetDirection(mouseDirection);

		return newProjectile;
	}
}
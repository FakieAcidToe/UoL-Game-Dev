using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField] Projectile projectilePrefab;
	[SerializeField] float interval = 2f; // Time in seconds between each bullet

	float timer = 0f;

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= interval)
		{
			SpawnProjectile();
			timer = 0f; // Reset timer
		}
	}

	public Projectile SpawnProjectile()
	{
		Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		// mouse direction from gameobject
		newProjectile.SetDirection((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));

		return newProjectile;
	}
}
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] Transform playerTransform;
	[SerializeField] EnemyMovement[] enemyPrefabs; // grab random enemy from array to spawn

	[Header("Distance around player to spawn enemies")]
	[SerializeField, Min(0)] float minRadius = 10f;
	[SerializeField, Min(0)] float maxRadius = 20f;

	[Header("Spawning properties")]
	[SerializeField] bool isSpawning = true;
	[SerializeField] float interval = 0.3f;

	void OnValidate()
	{
		if (minRadius > maxRadius) minRadius = maxRadius;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 pos = playerTransform == null ? transform.position : playerTransform.position;
		CircleGizmo.DrawCircle(pos, minRadius);
		CircleGizmo.DrawCircle(pos, maxRadius);
	}

	public void ToggleSpawning()
	{
		isSpawning = !isSpawning;
	}

	float timer = 0f;

	void Update()
	{
		if (isSpawning)
		{
			timer += Time.deltaTime;

			if (timer >= interval)
			{
				SpawnEnemy();
				timer = 0f; // Reset timer
			}
		}
	}

	public EnemyMovement SpawnEnemy()
	{
		float angle = Random.Range(0f, Mathf.PI * 2); // random angle in radians
		float distance = Random.Range(minRadius, maxRadius); // random distance

		return SpawnEnemy(new Vector3(
			playerTransform.position.x + Mathf.Cos(angle) * distance,
			playerTransform.position.y + Mathf.Sin(angle) * distance,
			playerTransform.position.z),
			enemyPrefabs[Mathf.FloorToInt(Random.value * enemyPrefabs.Length)]); // random enemy
	}

	public EnemyMovement SpawnEnemy(Vector3 _position, EnemyMovement enemy)
	{
		EnemyMovement newEnemy = Instantiate(enemy, _position, Quaternion.identity);
		newEnemy.SetTarget(playerTransform);
		return newEnemy;
	}
}
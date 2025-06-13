using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] Transform playerTransform;
	[SerializeField] EnemyMovement enemyPrefab;

	[Header("Distance around player to spawn enemies")]
	[SerializeField, Min(0)] float minRadius = 10f;
	[SerializeField, Min(0)] float maxRadius = 20f;

	void OnValidate()
	{
		if (minRadius > maxRadius) minRadius = maxRadius;
	}

	public void SpawnEnemyRandom() // temp function for buttons
	{
		SpawnEnemy();
	}

	public EnemyMovement SpawnEnemy()
	{
		float angle = Random.Range(0f, Mathf.PI * 2); // random angle in radians
		float distance = Random.Range(minRadius, maxRadius); // random distance

		return SpawnEnemy(new Vector3(
			playerTransform.position.x + Mathf.Cos(angle) * distance,
			playerTransform.position.y + Mathf.Sin(angle) * distance,
			playerTransform.position.z)
			);
	}

	public EnemyMovement SpawnEnemy(Vector3 _position)
	{
		EnemyMovement newEnemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
		newEnemy.SetTarget(playerTransform);
		return newEnemy;
	}
}
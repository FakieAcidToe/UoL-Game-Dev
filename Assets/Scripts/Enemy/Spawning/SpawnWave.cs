using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWave", menuName = "ScriptableObjects/Enemy Spawn Wave")]
public class SpawnWave : ScriptableObject
{
	enum WaveType
	{
		Donut,
		HorizontalLine,
		VerticalLine
	}

	[Header("Enemy References")]
	[SerializeField] EnemyMovement[] enemyPrefabs; // grab random enemy from array to spawn

	[Header("Wave Timing Properties")]
	[SerializeField, Tooltip("How long (seconds) does this wave last?\n0 = Infinite"), Min(0)] float enemyWaveTime = 20f;
	[SerializeField, Min(0)] float spawnInterval = 0.3f;
	[SerializeField, Tooltip("Number of intervals within this wave\n0 = Infinite"), Min(0)] int numIntervals = 0;

	[Header("Spawn Location Properties")]
	[SerializeField, Tooltip("What shape will the enemies spawn in?")] WaveType waveType = WaveType.Donut;
	[SerializeField, Min(0), Tooltip("Number of enemies to spawn per interval")] int spawnNumber = 1;
	[SerializeField, Min(0), Tooltip("True: Spawn `spawnNumber` enemies uniformly\nFalse: Spawn randomly")] bool spawnInOrder = false;
	[SerializeField, Tooltip("Random range that enemies spawn")] float minDist = 10f;
	[SerializeField, Tooltip("Random range that enemies spawn")] float maxDist = 20f;
	[SerializeField, Tooltip("Randomly make `minDist` and `maxDist` negative when spawning")] bool randomNegativeDist = false;
	[SerializeField, Tooltip("Range that enemy intervals spawn for Line wave types")] float spawnLength = 10f;

	float spawnTimer = 0f;
	float waveTimer = 0f;
	int intervals = 0;

	public void OnDrawGizmosSelected(Vector3 pos)
	{
		Gizmos.color = Color.yellow;

		switch (waveType)
		{
			case WaveType.Donut:
				CircleGizmo.DrawCircle(pos, minDist);
				CircleGizmo.DrawCircle(pos, maxDist);
				break;
			case WaveType.HorizontalLine:
				Gizmos.DrawWireCube(pos + new Vector3(0, (minDist + maxDist) / 2), new Vector3(spawnLength, maxDist - minDist));
				Gizmos.DrawLine(pos + new Vector3(-spawnLength / 2, (minDist + maxDist) / 2), pos + new Vector3(spawnLength / 2, (minDist + maxDist) / 2));
				if (randomNegativeDist)
				{
					Gizmos.DrawWireCube(pos + new Vector3(0, -(minDist + maxDist) / 2), new Vector3(spawnLength, maxDist - minDist));
					Gizmos.DrawLine(pos + new Vector3(-spawnLength / 2, -(minDist + maxDist) / 2), pos + new Vector3(spawnLength / 2, -(minDist + maxDist) / 2));
				}
				break;
			case WaveType.VerticalLine:
				Gizmos.DrawWireCube(pos + new Vector3((minDist + maxDist) / 2, 0), new Vector3(maxDist - minDist, spawnLength));
				Gizmos.DrawLine(pos + new Vector3((minDist + maxDist) / 2, -spawnLength / 2), pos + new Vector3((minDist + maxDist) / 2, spawnLength / 2));
				if (randomNegativeDist)
				{
					Gizmos.DrawWireCube(pos + new Vector3(-(minDist + maxDist) / 2, 0), new Vector3(maxDist - minDist, spawnLength));
					Gizmos.DrawLine(pos + new Vector3(-(minDist + maxDist) / 2, -spawnLength / 2), pos + new Vector3(-(minDist + maxDist) / 2, spawnLength / 2));
				}
				break;
		}
	}

	public void Reset()
	{
		spawnTimer = 0f;
		waveTimer = 0f;
		intervals = 0;
	}

	public void SpawnUpdate(Transform playerTransform)
	{
		if (enemyWaveTime > 0) waveTimer += Time.deltaTime;
		if ((numIntervals > 0 && intervals >= numIntervals) || enemyPrefabs.Length <= 0) return;

		spawnTimer += Time.deltaTime;
		if (spawnTimer >= spawnInterval)
		{
			switch (waveType)
			{
				case WaveType.Donut:
					for (int i = 0; i < spawnNumber; ++i)
					{
						float donutAngle = spawnInOrder ? (Mathf.PI * 2 * i) / spawnNumber : Random.Range(0f, Mathf.PI * 2); // random angle in radians
						float donutDistance = GetRandomDist();

						SpawnEnemy(new Vector3(
							playerTransform.position.x + Mathf.Cos(donutAngle) * donutDistance,
							playerTransform.position.y + Mathf.Sin(donutAngle) * donutDistance,
							playerTransform.position.z),
							enemyPrefabs[Mathf.FloorToInt(Random.value * enemyPrefabs.Length)], // random enemy
							playerTransform);
					}
					break;

				case WaveType.HorizontalLine:
					float horizontalDist = GetRandomDist();
					for (int i = 0; i < spawnNumber; ++i)
					{
						SpawnEnemy(new Vector3(
							playerTransform.position.x +
								(spawnInOrder ? spawnLength / spawnNumber * i - spawnLength / 2 : Random.Range(-spawnLength / 2, spawnLength / 2)),
							playerTransform.position.y + horizontalDist,
							playerTransform.position.z),
							enemyPrefabs[Mathf.FloorToInt(Random.value * enemyPrefabs.Length)], // random enemy
							playerTransform);
					}
					break;

				case WaveType.VerticalLine:
					float verticalDist = GetRandomDist();
					for (int i = 0; i < spawnNumber; ++i)
					{
						SpawnEnemy(new Vector3(
							playerTransform.position.x + verticalDist,
							playerTransform.position.y +
								(spawnInOrder ? spawnLength / spawnNumber * i - spawnLength / 2 : Random.Range(-spawnLength / 2, spawnLength / 2)),
							playerTransform.position.z),
							enemyPrefabs[Mathf.FloorToInt(Random.value * enemyPrefabs.Length)], // random enemy
							playerTransform);
					}
					break;
			}
			spawnTimer = 0f; // Reset timer
			++intervals;
		}
	}

	public bool IsWaveDone()
	{
		return enemyWaveTime > 0 && waveTimer >= enemyWaveTime;
	}

	void OnValidate()
	{
		if (minDist > maxDist) minDist = maxDist;
	}

	public EnemyMovement SpawnEnemy(Vector3 _position, EnemyMovement enemy, Transform playerTransform)
	{
		EnemyMovement newEnemy = Instantiate(enemy, _position, Quaternion.identity);
		newEnemy.SetTarget(playerTransform);
		return newEnemy;
	}

	float GetRandomDist()
	{
		return (randomNegativeDist && Random.value > 0.5f) ? Random.Range(-minDist, -maxDist) : Random.Range(minDist, maxDist); // random distance
	}
}

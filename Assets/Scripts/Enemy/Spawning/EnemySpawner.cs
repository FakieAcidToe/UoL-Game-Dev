using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] Transform playerTransform;

	[Header("Spawning properties")]
	[SerializeField] SpawnWave[] spawnWaves;
	[SerializeField] bool isSpawning = true;

	int waveNumber = 0;
	[SerializeField] bool loopWaves = true;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 pos = playerTransform == null ? transform.position : playerTransform.position;

		foreach (SpawnWave wave in spawnWaves)
			wave.OnDrawGizmosSelected(pos);
	}

	public void ToggleSpawning()
	{
		isSpawning = !isSpawning;
	}

	void Start()
	{
		if (waveNumber < spawnWaves.Length)
			spawnWaves[waveNumber].Reset();
	}

	void Update()
	{
		if (isSpawning && waveNumber < spawnWaves.Length)
		{
			spawnWaves[waveNumber].SpawnUpdate(playerTransform);
			if (spawnWaves[waveNumber].IsWaveDone())
			{
				++waveNumber;
				if (waveNumber >= spawnWaves.Length && loopWaves) waveNumber = 0;

				if (waveNumber < spawnWaves.Length) spawnWaves[waveNumber].Reset();
			}
		}
	}
}
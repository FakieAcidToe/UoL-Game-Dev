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
		{
			switch (wave.waveType)
			{
				case SpawnWave.WaveType.Donut:
					CircleGizmo.DrawCircle(pos, wave.minDist);
					CircleGizmo.DrawCircle(pos, wave.maxDist);
					break;
				case SpawnWave.WaveType.HorizontalLine:
					Gizmos.DrawWireCube(pos + new Vector3(0, (wave.minDist + wave.maxDist)/2), new Vector3(wave.spawnLength, wave.maxDist - wave.minDist));
					Gizmos.DrawLine(pos + new Vector3(-wave.spawnLength/2, (wave.minDist + wave.maxDist) / 2), pos + new Vector3(wave.spawnLength / 2, (wave.minDist + wave.maxDist) / 2));
					break;
				case SpawnWave.WaveType.VerticalLine:
					Gizmos.DrawWireCube(pos + new Vector3((wave.minDist + wave.maxDist)/2, 0), new Vector3(wave.maxDist - wave.minDist, wave.spawnLength));
					Gizmos.DrawLine(pos + new Vector3((wave.minDist + wave.maxDist) / 2, -wave.spawnLength / 2), pos + new Vector3((wave.minDist + wave.maxDist) / 2, wave.spawnLength / 2));
					break;
			}
		}
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
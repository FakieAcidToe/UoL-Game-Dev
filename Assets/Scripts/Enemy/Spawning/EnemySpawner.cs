using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
	[System.Serializable]
	class SpawnWaveSet
	{
		public string name;
		public int hpBuff;
		public float movementBuff;
		public int damageBuff;
		public SpawnWave[] spawnWaveSet;
	}

	[SerializeField] Transform playerTransform;
	[SerializeField] Text timerText;

	[Header("Spawning properties")]
	[SerializeField] SpawnWaveSet[] spawnWavesPerMinute;
	[SerializeField] bool isSpawning = true;
	
	[Header("Stage Properties")]
	[SerializeField] GameObject stage1;
	[SerializeField] GameObject stage2;
	GameObject currentStage;
	[SerializeField] int changeStageAtWave = 4;

	SpawnWaveSet currentSpawnWaveSet;

	int waveNumber = 0;
	[SerializeField] bool loopWaves = true;

	float timer = 0;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 pos = playerTransform == null ? transform.position : playerTransform.position;

		//foreach (SpawnWave wave in spawnWaves)
		//	wave.OnDrawGizmosSelected(pos);
	}

	public void ToggleSpawning()
	{
		isSpawning = !isSpawning;
	}

	void Start()
	{
		foreach (SpawnWaveSet spawnWaveSet in spawnWavesPerMinute)
			foreach (SpawnWave spawnWave in spawnWaveSet.spawnWaveSet)
				spawnWave.Reset();

		currentStage = Instantiate(stage1);
	}

	void Update()
	{
		if (Time.timeScale <= 0) return;

		timer += Time.deltaTime;
		timerText.text = TimeSpan.FromSeconds(timer).ToString("mm\\:ss");

		SpawnWaveSet prevSpawnWave = currentSpawnWaveSet;
		int currentMinute = Mathf.FloorToInt(timer / 60);
		currentSpawnWaveSet = spawnWavesPerMinute[Mathf.Clamp(Mathf.FloorToInt(timer / 60), 0, spawnWavesPerMinute.Length - 1)];
		if (currentSpawnWaveSet != prevSpawnWave) // new wave
		{
			waveNumber = 0;
			if (currentMinute == changeStageAtWave) // change stage
			{
				Destroy(currentStage);
				currentStage = Instantiate(stage2, playerTransform.position, Quaternion.identity);
			}
		}

		if (isSpawning && waveNumber < currentSpawnWaveSet.spawnWaveSet.Length)
		{
			currentSpawnWaveSet.spawnWaveSet[waveNumber].SpawnUpdate(playerTransform, currentSpawnWaveSet.hpBuff, currentSpawnWaveSet.movementBuff, currentSpawnWaveSet.damageBuff);
			if (currentSpawnWaveSet.spawnWaveSet[waveNumber].IsWaveDone())
			{
				++waveNumber;
				if (waveNumber >= currentSpawnWaveSet.spawnWaveSet.Length && loopWaves) waveNumber = 0;

				if (waveNumber < currentSpawnWaveSet.spawnWaveSet.Length) currentSpawnWaveSet.spawnWaveSet[waveNumber].Reset();
			}
		}
	}

	public void AddTimer(float time)
	{
		timer += time;
		timerText.text = TimeSpan.FromSeconds(timer).ToString("mm\\:ss");
	}
}
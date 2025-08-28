using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
	[System.Serializable]
	class Drops
	{
		public string name;
		public GameObject itemPrefab;
		public float dropRate;
	}

	[SerializeField] List<Drops> drops;

	public void SpawnPossibleDrops()
	{
		float randomNumber = Random.Range(0f, 100f);
		List<Drops> possibleDrops = new List<Drops>();

		foreach (Drops rate in drops)
		{
			if (randomNumber <= rate.dropRate)
			{
				possibleDrops.Add(rate);
			}
		}
		// Check the drop pool for possible drops
		if (possibleDrops.Count > 0)
		{
			Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
			Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
		}
	}
}

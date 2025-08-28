using System.Collections;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
	[Header("XP Stats")]
	[SerializeField] int experienceGranted;

	[Header("Easing")]
	[SerializeField] float collectionTime = 0.5f;
	Coroutine collectionCoroutine;

	public void Collect()
    {
        PlayerExperience player = FindObjectOfType<PlayerExperience>();
		if (player != null) Collect(player);
	}

	public void Collect(PlayerExperience player)
	{
		if (collectionCoroutine == null)
			collectionCoroutine = StartCoroutine(CollectCoroutine(player));
	}

	IEnumerator CollectCoroutine(PlayerExperience player)
	{
		float currentTime = 0;
		Vector2 startPos = transform.position;
		while (currentTime < collectionTime)
		{
			currentTime += Time.deltaTime;
			transform.position = new Vector2(
				EaseUtils.Interpolate(currentTime / collectionTime, startPos.x, player.transform.position.x, EaseUtils.EaseInSine),
				EaseUtils.Interpolate(currentTime / collectionTime, startPos.y, player.transform.position.y, EaseUtils.EaseInSine));
			yield return null;
		}

		player.IncreaseExperience(experienceGranted);
		Destroy(gameObject);
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeCover : MonoBehaviour
{
	Image cover;

	void Awake()
	{
		cover = GetComponent<Image>();
	}

	public void SetColor(Color color)
	{
		cover.color = color;
	}

	public IEnumerator Fade(float startAlpha, float endAlpha, float fadeDuration)
	{
		float timeElapsed = 0f;

		Color color = cover.color;
		color.a = startAlpha;
		cover.color = color;

		while (timeElapsed < fadeDuration)
		{
			timeElapsed += Time.deltaTime;
			color.a = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
			cover.color = color;
			yield return null;
		}

		color.a = endAlpha;
		cover.color = color;
	}
}
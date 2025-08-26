using UnityEngine;

public class FadeWhenBehind : MonoBehaviour
{
	[SerializeField] float fadedAlpha = 0f;
	[SerializeField] float fadeSpeed = 5f;
	SpriteRenderer spriteRenderer;
	float targetAlpha = 1f;
	Color originalColor;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			targetAlpha = fadedAlpha;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			targetAlpha = 1f;
	}

	void Update()
	{
		spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b,
			Mathf.Lerp(spriteRenderer.color.a, targetAlpha, Time.deltaTime * fadeSpeed));
	}
}
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
	[SerializeField] Sprite[] sprites;
	[SerializeField, Min(0)] float animationSpeed = 0.1f;
	float animationTimer = 0;
	int animationIndex = 0;

	[SerializeField] SpriteRenderer spriteRenderer;

	void Awake()
	{
		if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (animationSpeed <= 0 || sprites.Length <= 1) return;

		animationTimer += Time.deltaTime;
		if (animationTimer >= animationSpeed)
		{
			animationTimer -= animationSpeed;
			animationIndex = (animationIndex + 1) % sprites.Length;
			spriteRenderer.sprite = sprites[animationIndex];
		}
	}
}
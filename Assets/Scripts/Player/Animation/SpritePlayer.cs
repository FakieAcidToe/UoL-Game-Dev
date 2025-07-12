using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritePlayer : MonoBehaviour
{
	[Header("Sprite Properties")]
	[SerializeField, Tooltip("Hitbox sprite set")] Sprite[] sprites;
	[SerializeField, Tooltip("Time (seconds) per sprite"), Min(0)] float animationSpeed = 0.06f;

	// sprites
	SpriteRenderer sr;
	float animationTimer = 0f;
	int imageIndex = 0;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		if (sprites.Length > 0)
			sr.sprite = sprites[0];
	}

	void Update()
	{
		if (sprites.Length > 0 && animationSpeed > 0f)
		{
			animationTimer += Time.deltaTime;
			if (animationTimer > animationSpeed)
			{
				animationTimer -= animationSpeed;
				imageIndex = (imageIndex + 1) % sprites.Length;
				sr.sprite = sprites[imageIndex];
			}
		}
	}
}
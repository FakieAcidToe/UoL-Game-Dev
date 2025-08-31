using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImagePlayer : MonoBehaviour
{
	[Header("Sprite Properties")]
	[SerializeField, Tooltip("Hitbox sprite set")] Sprite[] sprites;
	[SerializeField, Tooltip("Time (seconds) per sprite"), Min(0)] float animationSpeed = 0.06f;

	// sprites
	Image image;
	float animationTimer = 0f;
	int imageIndex = 0;

	void Awake()
	{
		image = GetComponent<Image>();
		if (sprites.Length > 0)
			image.sprite = sprites[0];
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
				image.sprite = sprites[imageIndex];
			}
		}
	}
}
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
	[SerializeField, Min(0)] int health = 100;
	[SerializeField, Min(1)] int maxHealth = 100;

	[SerializeField] float lerpSpeed = 20f;

	float visualHealth = 0;
	bool shouldLerpHealth = false;

	[SerializeField] RectTransform fillBarMask;
	[SerializeField] RectTransform scrollingTexture;
	[SerializeField] Vector2 scrollSpeed;

	Image scrollingTextureImage;
	Vector2 offset;

	void Awake()
	{
		visualHealth = health;

		scrollingTextureImage = scrollingTexture.GetComponent<Image>();
	}

	void Update()
	{
		if (shouldLerpHealth)
		{
			// lerp to current health
			if (Mathf.Abs(visualHealth - health) < 1)
			{
				visualHealth = health;
				shouldLerpHealth = false;
				UpdateMaskPosition();
			}
			else
			{
				visualHealth = Mathf.Lerp(visualHealth, health, 1f - Mathf.Exp(-lerpSpeed * Time.deltaTime));
				UpdateMaskPosition();
			}
		}

		// scroll healthbar bg
		offset += scrollSpeed * Time.deltaTime;
		scrollingTextureImage.materialForRendering.SetTextureOffset("_MainTex", offset); // use materialForRendering because it's under a mask
	}

	public void SetHealthRelative(int _healthAdd)
	{
		SetHealth(health + _healthAdd);
	}

	public void SetHealth(int _health)
	{
		health = Mathf.Clamp(_health, 0, maxHealth);
		shouldLerpHealth = true;
	}

	void UpdateMaskPosition()
	{
		if (fillBarMask != null && scrollingTexture != null)
		{
			float x = Mathf.Lerp(-fillBarMask.rect.width, 0, Mathf.Clamp(visualHealth / maxHealth, 0, maxHealth));
			fillBarMask.localPosition = new Vector3(x, 0, 0);
			scrollingTexture.localPosition = new Vector3(-x, 0, 0);
		}
	}

	void OnValidate()
	{
		visualHealth = health;
		UpdateMaskPosition();
	}
}

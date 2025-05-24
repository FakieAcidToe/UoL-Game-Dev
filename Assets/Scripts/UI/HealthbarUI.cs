using UnityEngine;

public class HealthbarUI : MonoBehaviour
{
	[SerializeField, Min(0)] int health = 100;
	[SerializeField, Min(1)] int maxHealth = 100;

	[SerializeField] float lerpSpeed = 20f;

	float visualHealth = 0;
	bool shouldLerpHealth = false;

	[SerializeField] RectTransform fillBarMask;
	[SerializeField] RectTransform fillBar;

	void Awake()
	{
		visualHealth = health;
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
		if (fillBarMask != null && fillBar != null)
		{
			float x = Mathf.Lerp(-fillBarMask.rect.width, 0, Mathf.Clamp(visualHealth / maxHealth, 0, maxHealth));
			fillBarMask.localPosition = new Vector3(x, 0, 0);
			fillBar.localPosition = new Vector3(-x, 0, 0);
		}
	}

	void OnValidate()
	{
		visualHealth = health;
		UpdateMaskPosition();
	}
}

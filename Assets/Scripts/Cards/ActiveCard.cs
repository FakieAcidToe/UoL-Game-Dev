using UnityEngine;
using UnityEngine.UI;

public class ActiveCard : AbstractCard
{
	[Header("Cooldowns"), SerializeField, Min(0)]
	float cooldownTime = 1f; // how long the cooldown is
	float cooldown; // timer for cooldown
	[SerializeField, Range(1, 255)] int cooldownAlpha = 64; // alpha colour of mask when in cooldown

	[Header("GameObject References")]
	[SerializeField] Image cooldownShadeMask;
	[SerializeField] Image cooldownShadeImage;

	[Header("Audio")]
	[SerializeField] protected AudioClip sfx;

	protected override void Start()
	{
		if (PlayerStatus.Instance != null)
			cooldownTime *= 1 - PlayerStatus.Instance.playerCD;

		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		// cooldowns
		if (cooldown > 0 && cooldownTime > 0)
		{
			cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);
			cooldownShadeImage.rectTransform.localPosition = new Vector3(0, Mathf.Lerp(-cooldownShadeImage.rectTransform.rect.height, 0, cooldown / cooldownTime), 0);
			cooldownShadeMask.color = new Color(
				cooldownShadeMask.color.r,
				cooldownShadeMask.color.g,
				cooldownShadeMask.color.b,
				(CanBeUsed() ? 1f : cooldownAlpha) / 255f);
		}
	}

	public virtual void UseCardEffect()
	{
		if (CanBeUsed())
			TriggerCooldown();
	}

	public void TriggerCooldown()
	{
		if (sfx != null) SoundManager.Instance.Play(sfx);
		cooldown = cooldownTime;
	}

	public bool CanBeUsed()
	{
		return cooldown <= 0;
	}
}

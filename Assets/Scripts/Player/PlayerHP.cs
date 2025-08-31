using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
	[Header("HP")]
	[SerializeField] int maxHp = 100;
	int hp;
	public HealthbarUI healthbar;
	[SerializeField] FadeCover hurtCover;
	[SerializeField] Color hurtColor;
	[SerializeField] Color healColor;
	Coroutine hurtCoverCoroutine;

	[Header("Unity Events")]
	public UnityEvent onDamaged;
	public UnityEvent onZeroHP;

	public void SetHP(int _hp)
	{
		hp = _hp;
		healthbar.SetHealth(hp, false);
	}

	public void SetMaxHP(int _maxHp)
	{
		maxHp = _maxHp;
		healthbar.SetMaxHealth(maxHp, false);
	}

	void Start()
	{
		hp = maxHp;
		healthbar.SetHealth(hp, false);
		healthbar.SetMaxHealth(maxHp, false);
	}

	public void TakeDamage(int damage)
	{
		hp = Mathf.Clamp(hp - damage, 0, maxHp);
		healthbar.SetHealth(hp);

		onDamaged.Invoke();

		if (hurtCoverCoroutine != null) StopCoroutine(hurtCoverCoroutine);
		hurtCoverCoroutine = StartCoroutine(HurtCoverCoroutine(damage > 0 ? hurtColor : healColor));

		if (hp <= 0) onZeroHP.Invoke();
	}

	IEnumerator HurtCoverCoroutine(Color color)
	{
		if (hurtCover == null) yield break;
		hurtCover.SetColor(color);
		yield return hurtCover.Fade(0, 0.25f, 0.05f);
		yield return hurtCover.Fade(0.25f, 0, 0.15f);
	}
}
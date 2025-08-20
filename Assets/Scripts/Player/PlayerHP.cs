using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
	[Header("HP")]
	[SerializeField] int maxHp = 100;
	int hp;
	public HealthbarUI healthbar;

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
		if (hp <= 0) onZeroHP.Invoke();
	}
}

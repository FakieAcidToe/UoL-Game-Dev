using UnityEngine;

public class PlayerHP : MonoBehaviour
{
	[Header("HP")]
	[SerializeField] int maxHp = 100;
	int hp;
	public HealthbarUI healthbar;

	void Awake()
	{
		hp = maxHp;
	}

	public void SetHP(int _hp)
	{
		hp = _hp;
		healthbar.SetHealth(hp, false);
	}

	public void SetMaxHP(int _maxHp)
	{
		maxHp = _maxHp;
		healthbar.SetMaxHealth(hp, false);
	}

	void Start()
	{
		healthbar.SetHealth(hp, false);
		healthbar.SetMaxHealth(hp, false);
	}

	public void TakeDamage(int damage)
	{
		hp = Mathf.Clamp(hp - damage, 0, maxHp);
		healthbar.SetHealth(hp);
	}
}

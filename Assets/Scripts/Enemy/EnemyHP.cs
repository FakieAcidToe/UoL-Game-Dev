using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHP : MonoBehaviour
{
	int currentHP;
	[SerializeField] int maxHP = 3;
	[SerializeField] UnityEvent onDeath;

	public EnemyMovement movement { get; private set; }

	void Awake()
	{
		currentHP = maxHP;
		movement = GetComponent<EnemyMovement>();
	}

	public void TakeDamage(int damageAmount)
	{
		currentHP -= damageAmount;

		if (currentHP <= 0)
		{
			currentHP = 0;
			Die();
		}
	}

	public void Die()
	{
		onDeath.Invoke();
		Destroy(gameObject);
	}

	public void AddMaxHP(int hpBoost)
	{
		maxHP += hpBoost;
		currentHP += hpBoost;
	}
}
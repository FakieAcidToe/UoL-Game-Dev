using UnityEngine;

public class EnemyHP : MonoBehaviour
{
	int currentHP;
	[SerializeField] int maxHP = 3;

	EnemyMovement movement;

	void Awake()
	{
		currentHP = maxHP;
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
		Destroy(gameObject);
	}
}
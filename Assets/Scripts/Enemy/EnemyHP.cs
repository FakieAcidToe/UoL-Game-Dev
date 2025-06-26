using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHP : MonoBehaviour
{
	int currentHP;
	[SerializeField] int maxHP = 3;

	public EnemyMovement movement { get; private set; }

	void Awake()
	{
		currentHP = maxHP;
		movement = GetComponent<EnemyMovement>();
	}

	public void TakeDamage(int damageAmount)
	{
		if (movement.IsInHitstun()) return;

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
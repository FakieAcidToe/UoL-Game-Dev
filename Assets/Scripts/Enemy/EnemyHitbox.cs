using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
	[SerializeField, Min(0)] int damage = 1;
	[SerializeField, Min(0)] float tickRate = 0.1f;

	float attackTimer = 0;

	void OnTriggerStay2D(Collider2D collision)
	{
		if (attackTimer <= 0)
		{
			PlayerHP playerHP = collision.GetComponent<PlayerHP>();
			if (playerHP != null)
			{
				int reducedDamage = damage;
				if (PlayerStatus.Instance != null)
					reducedDamage = Mathf.CeilToInt(damage * (1 - PlayerStatus.Instance.playerDMGR));

				playerHP.TakeDamage(reducedDamage);
				DamageNumberSpawner.Instance.SpawnDamageNumbers(
					reducedDamage,
					Vector2.Lerp(transform.position, playerHP.transform.position, 0.5f),
					Color.magenta);

				attackTimer = tickRate;
			}
		}
	}

	void Update()
	{
		if (attackTimer > 0) attackTimer -= Time.deltaTime;
	}
}
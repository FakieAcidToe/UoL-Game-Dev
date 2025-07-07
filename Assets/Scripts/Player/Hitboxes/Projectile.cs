using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Hitbox
{
	[Header("Projectile Properties")]
	[SerializeField, Tooltip("Projectile movement speed")]
	float speed = 1f;
	[SerializeField, Tooltip("How many enemies the projectile can hit before dying, -1 = infinite pierce"), Min(-1)]
	int pierce = 0;

	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHP enemyHP = collision.gameObject.GetComponent<EnemyHP>();
		if (enemyHP != null)
		{
			enemyHP.TakeDamage(damage); // damage enemy
			enemyHP.movement.ReceiveKnockback(direction.normalized * knockback, hitstun);

			if (pierce > -1 && --pierce < 0) Destroy(gameObject); // handle projectile piercing
		}
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
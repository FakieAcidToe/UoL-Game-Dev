using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField, Tooltip("Projectile movement speed")]
	float speed = 1f;
	[SerializeField, Tooltip("How long the projectile lasts"), Min(0)]
	float lifetime = 2f;
	[SerializeField, Tooltip("How many enemies the projectile can hit before dying, -1 = infinite pierce"), Min(-1)]
	int pierce = 0;
	[SerializeField, Tooltip("Damage amount to deal on hit")]
	int damage = 1;
	[SerializeField, Tooltip("Knockback impulse strength applied on hit")]
	float knockback = 0.5f;
	[SerializeField, Tooltip("Hitstun duration applied on hit")]
	float hitstun = 0.05f;

	Rigidbody2D rb;
	Vector2 direction;
	float lifetimeTimer = 0f;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void SetDirection(Vector2 _direction)
	{
		direction = _direction.normalized;
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

	void Update()
	{
		lifetimeTimer += Time.deltaTime;

		if (lifetimeTimer >= lifetime) Destroy(gameObject);
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
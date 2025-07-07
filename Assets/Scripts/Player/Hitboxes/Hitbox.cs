using UnityEngine;

public class Hitbox : MonoBehaviour
{
	[Header("Base Hitbox Properties")]
	[SerializeField, Tooltip("How long the projectile lasts"), Min(0)]
	protected float lifetime = 2f;
	[SerializeField, Tooltip("Damage amount to deal on hit")]
	protected int damage = 1;
	[SerializeField, Tooltip("Knockback impulse strength applied on hit")]
	protected float knockback = 0.5f;
	[SerializeField, Tooltip("Hitstun duration applied on hit")]
	protected float hitstun = 0.05f;

	protected Vector2 direction = Vector2.zero;
	protected float lifetimeTimer = 0f;

	public Vector2 GetDirection()
	{
		return direction;
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
		}
	}

	void Update()
	{
		lifetimeTimer += Time.deltaTime;

		if (lifetimeTimer >= lifetime) Destroy(gameObject);
	}
}
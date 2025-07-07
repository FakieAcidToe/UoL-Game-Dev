using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
	[Header("Base Hitbox Properties")]
	[SerializeField, Tooltip("How long the projectile lasts"), Min(0)]
	float lifetime = 2f;
	[SerializeField, Tooltip("Damage amount to deal on hit")]
	protected int damage = 1;
	[SerializeField, Tooltip("Knockback impulse strength applied on hit")]
	protected float knockback = 0.5f;
	[SerializeField, Tooltip("Hitstun duration applied on hit")]
	protected float hitstun = 0.05f;
	[SerializeField, Tooltip("How often hitEnemies list should be cleared to reenable them to be hit again\nNegative = Don't reenable"), Min(-1)]
	float hitboxLockout = -1f;

	protected Vector2 direction = Vector2.zero;
	protected float lifetimeTimer = 0f;

	protected List<EnemyHP> hitEnemies; // lockout
	float lockoutTimer = 0f;

	protected virtual void Awake()
	{
		hitEnemies = new List<EnemyHP>();
	}

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
		if (enemyHP != null && !hitEnemies.Contains(enemyHP))
			DamageEnemy(enemyHP);
	}

	protected void DamageEnemy(EnemyHP _enemy)
	{
		_enemy.TakeDamage(damage); // damage enemy
		_enemy.movement.ReceiveKnockback(direction.normalized * knockback, hitstun);
		hitEnemies.Add(_enemy);
	}

	void Update()
	{
		lifetimeTimer += Time.deltaTime;

		if (hitboxLockout >= 0) // clear lockout
		{
			lockoutTimer += Time.deltaTime;
			if (lockoutTimer >= hitboxLockout)
			{
				lockoutTimer = 0;
				hitEnemies.Clear();
			}
		}

		if (lifetimeTimer >= lifetime) Destroy(gameObject);
	}
}
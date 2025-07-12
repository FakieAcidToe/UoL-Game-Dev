using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
	[Header("Base Hitbox Properties")]
	[SerializeField, Tooltip("How long the hitbox lasts"), Min(0)]
	float lifetime = 2f;
	[SerializeField, Tooltip("Damage amount to deal on hit")]
	protected int damage = 1;
	[SerializeField, Tooltip("Knockback impulse strength applied on hit")]
	protected float knockback = 0.5f;
	[SerializeField, Tooltip("Hitstun duration applied on hit")]
	protected float hitstun = 0.05f;
	[SerializeField, Tooltip("How often hitEnemies list should be cleared to reenable them to be hit again\nNegative = Don't reenable"), Min(-1)]
	float hitboxLockout = -1f;
	[SerializeField, Tooltip("Time before hitbox becomes active (for melee hitbox startup animation sprite)"), Min(0)]
	float hitboxDelay = 0f;

	protected Vector2 direction = Vector2.zero;
	protected float lifetimeTimer = 0f;

	Collider2D hitboxCollider;

	protected List<EnemyHP> hitEnemies; // lockout
	float lockoutTimer = 0f;

	protected virtual void Awake()
	{
		hitEnemies = new List<EnemyHP>();
		hitboxCollider = GetComponent<Collider2D>();
		if (hitboxDelay > 0) hitboxCollider.enabled = false;
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

	protected virtual void DamageEnemy(EnemyHP _enemy)
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

		if (lifetimeTimer >= hitboxDelay)
			hitboxCollider.enabled = true;

		if (lifetimeTimer >= lifetime) Destroy(gameObject);
	}
}
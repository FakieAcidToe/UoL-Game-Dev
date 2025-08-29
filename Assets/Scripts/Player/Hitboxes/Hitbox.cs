using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
	enum KnockbackTypes
	{
		Direction,
		FromCenter,
		FromParent
	}

	[Header("Base Hitbox Properties")]
	[SerializeField, Tooltip("How long the hitbox lasts\n0 = Infinity"), Min(0)]
	float lifetime = 2f;
	[SerializeField, Tooltip("Damage amount to deal on hit")]
	int damage = 10;
	[SerializeField, Min(1), Tooltip("Amount to multiply damage when landing critical hits")]
	float critDamageMult = 2;
	[SerializeField, Tooltip("What direction to knock enemies")]
	KnockbackTypes knockbackType = KnockbackTypes.Direction;
	[SerializeField, Tooltip("Knockback impulse strength applied on hit")]
	float knockback = 0.5f;
	[SerializeField, Tooltip("Hitstun duration applied on hit")]
	float hitstun = 0.05f;
	[SerializeField, Tooltip("How often hitEnemies list should be cleared to reenable them to be hit again\nNegative = Don't reenable"), Min(-1)]
	float hitboxLockout = -1f;
	[SerializeField, Tooltip("Time before hitbox becomes active (for melee hitbox startup animation sprite)"), Min(0)]
	float hitboxDelay = 0f;
	[SerializeField, Tooltip("How many enemies the hitbox can hit before dying, -1 = infinite pierce"), Min(-1)]
	int pierce = 0;
	[SerializeField, Tooltip("How long Screenshake lasts on hit"), Min(0)]
	float screenshakeDuration = 0.06f;
	[SerializeField, Tooltip("How powerful the Screenshake feels"), Min(0)]
	float screenshakeMagnitude = 0.03f;
	[SerializeField]
	AudioClip sfx;

	protected Vector2 direction = Vector2.zero;
	float lifetimeTimer = 0f;

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

	public void SetDirection(Vector2 _direction, bool alsoSetQuaternion = false)
	{
		direction = _direction.normalized;

		if (alsoSetQuaternion) transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		EnemyHP enemyHP = collision.gameObject.GetComponent<EnemyHP>();
		if (enemyHP != null && !hitEnemies.Contains(enemyHP))
			DamageEnemy(enemyHP);
	}

	protected virtual void DamageEnemy(EnemyHP _enemy)
	{
		float increasedDamage = damage;
		bool hasCrit = false;
		if (PlayerStatus.Instance != null)
		{
			increasedDamage *= PlayerStatus.Instance.playerDMGB;
			if (critDamageMult > 1 && Random.value < PlayerStatus.Instance.playerCrit)
			{
				increasedDamage *= critDamageMult;
				hasCrit = true;
			}
		}

		_enemy.TakeDamage(Mathf.FloorToInt(increasedDamage)); // damage enemy

		// damage numbers
		if (increasedDamage > 0)
			DamageNumberSpawner.Instance.SpawnDamageNumbers(Mathf.FloorToInt(increasedDamage), _enemy.transform.position, hasCrit ? Color.cyan : Color.red);

		Vector2 knockbackDirection;
		switch (knockbackType)
		{
			default:
			case KnockbackTypes.Direction:
				knockbackDirection = direction.normalized;
				break;
			case KnockbackTypes.FromCenter:
				knockbackDirection = (_enemy.transform.position - transform.position).normalized;
				break;
			case KnockbackTypes.FromParent:
				knockbackDirection = (_enemy.transform.position - transform.parent.position).normalized;
				break;
		}
		_enemy.movement.ReceiveKnockback(knockbackDirection * knockback, hitstun);
		hitEnemies.Add(_enemy);

		// screenshake
		ScreenShake.Instance.Shake(screenshakeDuration, screenshakeMagnitude);

		// audio
		if (sfx != null)
			SoundManager.Instance.Play(sfx);

		if (pierce > -1 && --pierce < 0) Destroy(gameObject); // handle projectile piercing
	}

	void Update()
	{
		lifetimeTimer += Time.deltaTime;

		if (hitboxLockout >= 0f) // clear lockout
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
	}

	void LateUpdate()
	{
		if (lifetime > 0 && lifetimeTimer >= lifetime) Destroy(gameObject);
	}
}
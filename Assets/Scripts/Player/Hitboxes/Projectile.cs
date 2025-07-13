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

	protected override void Awake()
	{
		base.Awake();

		rb = GetComponent<Rigidbody2D>();
	}

	protected override void DamageEnemy(EnemyHP _enemy)
	{
		base.DamageEnemy(_enemy);
		if (pierce > -1 && --pierce < 0) Destroy(gameObject); // handle projectile piercing
	}

	void FixedUpdate()
	{
		if (speed != 0)
			rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
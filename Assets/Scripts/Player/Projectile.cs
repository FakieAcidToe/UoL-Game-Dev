using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField] float speed = 1f; // projectile movement speed
	[SerializeField] float lifetime = 2f; // how long the projectile lasts
	[SerializeField] int pierce = 0; // how many enemies the projectile can hit before dying

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
		EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
		if (enemy != null)
		{
			enemy.Die(); // kill enemy
			if (--pierce < 0) Destroy(gameObject); // handle piercing
		}
	}

	void Update()
	{
		lifetimeTimer += Time.deltaTime;

		if (lifetimeTimer >= lifetime)
			Destroy(gameObject);
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
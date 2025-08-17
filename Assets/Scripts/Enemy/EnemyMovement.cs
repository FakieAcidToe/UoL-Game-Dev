using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] float moveSpeed = 2f;

	[Header("Knockback/Hitstun Multipliers")]
	[SerializeField, Min(0)] float knockbackAdj = 1f;
	[SerializeField, Min(0)] float hitstunAdj = 1f;

	Rigidbody2D rb;
	Vector2 movement;

	Transform target;

	float hitstun = 0; // time left in hitstun (cant move)

	public EnemyHP enemyHP { get; private set; }

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyHP = GetComponent<EnemyHP>();
	}

	public void SetTarget(Transform _target)
	{
		target = _target;
	}

	void Update()
	{
		if (IsInHitstun())
		{
			movement = Vector2.zero;
			hitstun -= Time.deltaTime;
			if (hitstun < 0) hitstun = 0;
		}
		else
		{
			if (target == null) movement = Vector2.zero;
			else movement = target.position - transform.position;

			// normalize diagonal movement
			if (movement.sqrMagnitude > 1) movement.Normalize();
		}
	}

	void FixedUpdate()
	{
		// move the player using physics
		if (!IsInHitstun())
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	public void ReceiveKnockback(Vector2 _force, float _hitstun)
	{
		rb.AddForce(_force * knockbackAdj, ForceMode2D.Impulse);
		hitstun = _hitstun * hitstunAdj;
	}

	public bool IsInHitstun()
	{
		return hitstun > 0;
	}
	public Vector2 GetMovement()
	{
		return movement.normalized;
	}
}
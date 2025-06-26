using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerMovement), typeof(ProjectileSpawner))]
public class PlayerAnimController : MonoBehaviour
{
	[SerializeField] PlayerAnimationSet animationSet;

	[Header("Animation Speed")]
	[SerializeField] float idleSpeed = 0.1f;

	float animationTimer = 0;
	bool frame = false;

	SpriteRenderer spriteRenderer;
	PlayerMovement movement;
	ProjectileSpawner spawner;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		movement = GetComponent<PlayerMovement>();
		spawner = GetComponent<ProjectileSpawner>();
		UpdateSprite();
	}

	void Update()
	{
		animationTimer += Time.deltaTime;

		if (animationTimer > idleSpeed)
		{
			animationTimer = 0;
			frame = !frame;
		}

		UpdateSprite();
	}

	void UpdateSprite()
	{
		float x = spawner.mouseDirection.x;
		float y = spawner.mouseDirection.y;

		if (Mathf.Abs(x) > Mathf.Abs(y))
		{
			spriteRenderer.flipX = (x < 0);

			// side
			spriteRenderer.sprite = frame ? animationSet.idleSide2 : animationSet.idleSide1;

		}
		else
		{
			spriteRenderer.flipX = false;

			if (y > 0) // back
				spriteRenderer.sprite = frame ? animationSet.idleBack2 : animationSet.idleBack1;
			else // front
				spriteRenderer.sprite = frame ? animationSet.idleFront2 : animationSet.idleFront1;
		}
	}
}
﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritePlayer : MonoBehaviour
{
	[Header("Sprite Properties")]
	[SerializeField, Tooltip("Hitbox sprite set")] Sprite[] sprites;
	[SerializeField, Tooltip("Time (seconds) per sprite"), Min(0)] float animationSpeed = 0.06f;
	[Header("Sprite Flipping")]
	[SerializeField] bool shouldFlipX = false;
	[SerializeField] bool facingRight = true;

	// sprites
	SpriteRenderer sr;
	float animationTimer = 0f;
	int imageIndex = 0;

	float lastXPos = 0;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		if (sprites.Length > 0)
			sr.sprite = sprites[0];
	}

	void Start()
	{
		lastXPos = transform.position.x;
	}

	void Update()
	{
		// sprite flip x
		if (shouldFlipX && lastXPos != transform.position.x)
		{
			sr.flipX = (lastXPos < transform.position.x) ^ facingRight;
			lastXPos = transform.position.x;
		}

		if (sprites.Length > 0 && animationSpeed > 0f)
		{
			animationTimer += Time.deltaTime;
			if (animationTimer > animationSpeed)
			{
				animationTimer -= animationSpeed;
				imageIndex = (imageIndex + 1) % sprites.Length;
				sr.sprite = sprites[imageIndex];
			}
		}
	}
}
using UnityEngine;

public class SpriteLayerSorter : MonoBehaviour
{
	[SerializeField, Tooltip("Null = find during Awake")] SpriteRenderer spriteRenderer;
	[SerializeField, Tooltip("Origin position of the sprite for depth calculation")] float yOffset = 0f;

	[SerializeField] bool applyEveryFrame = true;

	void Awake()
	{
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		UpdateSortingLayer();
	}

	void LateUpdate()
	{
		if (applyEveryFrame)
			UpdateSortingLayer();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position + Vector3.up*yOffset, 0.01f);
	}

	void UpdateSortingLayer()
	{
		spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y + yOffset) * -100);
	}
}
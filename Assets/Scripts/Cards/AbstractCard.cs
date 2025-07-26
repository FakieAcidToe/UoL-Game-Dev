using UnityEngine;
using UnityEngine.UI;

public class AbstractCard : MonoBehaviour
{
	[Header("On Spawn Properties")]
	[SerializeField] bool hasRandomColour = false;

	[Header("Card Movement"), Tooltip("How fast to lerp to target position and angle"), SerializeField]
	float lerpSpeed = 20f;
	Vector3 targetPosition;
	float targetAngle;

	[Tooltip("How close should it lerp to before it snaps to target and stop lerping"), SerializeField]
	float lerpLeeway = 0.01f;
	bool shouldLerpPos;
	bool shouldLerpRot;

	[Header("GameObject References"), SerializeField]
	Image backgroundImage;
	
	protected Transform playerObj;

	void Awake()
	{
		targetPosition.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		targetAngle = transform.localRotation.eulerAngles.z;
		shouldLerpPos = false;
		shouldLerpRot = false;
	}

	protected virtual void Start()
	{
		if (hasRandomColour) SetColour(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

		OnPickup();
	}

	protected virtual void Update()
	{
		if (shouldLerpPos)
		{
			// lerp position
			if (Vector3.Distance(transform.localPosition, targetPosition) < lerpLeeway)
			{
				transform.localPosition = targetPosition;
				shouldLerpPos = false;
			}
			else
				transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 1f - Mathf.Exp(-lerpSpeed * Time.deltaTime));
		}

		if (shouldLerpRot)
		{
			// lerp rotation
			if (Mathf.Abs(transform.localRotation.eulerAngles.z - targetAngle) < lerpLeeway)
			{
				transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
				shouldLerpRot = false;
			}
			else
				transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, targetAngle), 1f - Mathf.Exp(-lerpSpeed * Time.deltaTime));
		}
	}

	protected virtual void OnPickup()
	{
		return;
	}

	public void DestroySelf()
	{
		// idealy do a 'use' animation before destroying
		Destroy(gameObject);
	}

	public void SetColour(Color _color)
	{
		backgroundImage.color = _color;
	}

	public void SetPlayerTransform(Transform _player)
	{
		playerObj = _player;
	}

	public void SetTargetPosition(float _x, float _y)
	{
		targetPosition.x = _x;
		targetPosition.y = _y;
		shouldLerpPos = true;
	}

	public void SetTargetRotation(float _z)
	{
		targetAngle = _z;
		shouldLerpRot = true;
	}
}
using UnityEngine;
using UnityEngine.UI;

public class AbstractCard : MonoBehaviour
{
	Image image;

	[SerializeField] float lerpSpeed = 20f; // how fast to lerp to target position and angle
	Vector3 targetPosition;
	float targetAngle;

	[SerializeField] float lerpLeeway = 0.01f; // how close should it lerp to before it snaps to target and stop lerping
	bool shouldLerpPos;
	bool shouldLerpRot;

	void Awake()
	{
		image = GetComponent<Image>();
		targetPosition.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		targetAngle = transform.localRotation.eulerAngles.z;
		shouldLerpPos = false;
		shouldLerpRot = false;
	}

	void Update()
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

	public virtual void UseCardEffect()
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
		image.color = _color;
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

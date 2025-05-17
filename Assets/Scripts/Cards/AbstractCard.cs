using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractCard : MonoBehaviour
{
	Image image;

	[SerializeField] float lerpAmount = 0.1f;
	Vector3 targetPosition;
	float targetAngle;

	void Awake()
	{
		image = GetComponent<Image>();
		targetPosition.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		targetAngle = 0;
	}

	void Update()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, lerpAmount);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, targetAngle), lerpAmount);
	}

	public void SetColour(Color _color)
	{
		image.color = _color;
	}

	public void SetTargetPosition(float _x, float _y)
	{
		targetPosition.x = _x;
		targetPosition.y = _y;
	}

	public void SetTargetRotation(float _z)
	{
		targetAngle = _z;
	}
}

using UnityEngine;

public class SetCameraBackgroundColor : MonoBehaviour
{
	[Header("Background Color")]
	[SerializeField] Color backgroundColor = Color.black;

	void Awake()
	{
		SetBackgroundColor(backgroundColor);
	}

	public void SetBackgroundColor(Color _newColor)
	{
		Camera.main.backgroundColor = _newColor;
	}
}
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFader : MonoBehaviour
{
	[Header("Fade Properties")]
	[SerializeField] float fadeSpeed = -1f;
	[SerializeField, Range(0f, 1f)] float minFade = 0f;
	[SerializeField, Range(0f, 1f)] float maxFade = 1f;

	SpriteRenderer sr;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Clamp(sr.color.a + fadeSpeed * Time.deltaTime, minFade, maxFade));
	}
}
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	public void PlaySFX(AudioClip sfx)
	{
		if (SoundManager.Instance != null)
			SoundManager.Instance.Play(sfx);
	}
}
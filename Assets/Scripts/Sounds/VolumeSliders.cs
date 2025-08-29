using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	void Start()
	{
		musicSlider.value = SoundManager.Instance.GetMusicVolume();
		sfxSlider.value = SoundManager.Instance.GetSfxVolume();
	}

	void OnEnable()
	{
		musicSlider.onValueChanged.AddListener(MusicSliderChanged);
		sfxSlider.onValueChanged.AddListener(SfxSliderChanged);
	}

	void OnDisable()
	{
		musicSlider.onValueChanged.RemoveListener(MusicSliderChanged);
		sfxSlider.onValueChanged.RemoveListener(SfxSliderChanged);
	}

	void MusicSliderChanged(float value)
	{
		SoundManager.Instance.SetMusicVolume(value);
	}

	void SfxSliderChanged(float value)
	{
		SoundManager.Instance.SetSFXVolume(value);
	}
}

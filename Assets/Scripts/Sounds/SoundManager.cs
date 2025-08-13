using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	[Header("This Scene's Music Theme")]
	[SerializeField] AudioClip musicIntro;
	[SerializeField] AudioClip musicLoop;

	[Header("Scene References")]
	[SerializeField] AudioSource effectsSource;
	[SerializeField] AudioSource musicSource;

	public static SoundManager Instance = null;

	AudioClip bgmIntro = null;
	AudioClip bgmLoop = null;
	bool bgmInIntro = false;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Instance.PlayMusic(musicLoop, musicIntro);
		}
		else
		{
			Instance.PlayMusic(musicLoop, musicIntro);
			Destroy(gameObject);
		}
	}

	public void SetMusicVolume(float musicVolume)
	{
		SetMixerVolume(musicVolume, musicSource.outputAudioMixerGroup.audioMixer, "Music Volume");
	}

	public void SetSFXVolume(float sfxVolume)
	{
		SetMixerVolume(sfxVolume, effectsSource.outputAudioMixerGroup.audioMixer, "SFX Volume");
	}

	void SetMixerVolume(float sliderValue, AudioMixer audioMixer, string exposedName)
	{
		// from a slider of range 0 to 100. change normalizedValue accordingly.
		float normalizedValue = Mathf.InverseLerp(0, 100, sliderValue);
		float remappedValue = Mathf.Lerp(0.0001f, 1, normalizedValue);

		float dB = Mathf.Log10(Mathf.Clamp(remappedValue, 0.0001f, 1)) * 20;
		audioMixer.SetFloat(exposedName, dB);
	}

	void Update()
	{
		if (bgmInIntro && !musicSource.isPlaying)
		{
			PlayMusicLoop(bgmLoop);
		}
	}

	public void Play(AudioClip _clip)
	{
		effectsSource.clip = _clip;
		effectsSource.PlayOneShot(_clip);
	}

	public void PlayMusic(AudioClip _loop, AudioClip _intro = null, bool forceRestartIfSameTrack = false)
	{
		if (!forceRestartIfSameTrack && bgmLoop == _loop) // if same track
			return;

		bgmLoop = _loop;
		bgmIntro = _intro;
		if (bgmIntro != null)
		{
			PlayMusicLoop(bgmIntro);
			bgmInIntro = true;
			musicSource.loop = false;
		}
		else
		{
			PlayMusicLoop(bgmLoop);
		}
	}

	public void PlayMusicLoop(AudioClip _clip)
	{
		bgmInIntro = false;
		musicSource.loop = true;
		musicSource.clip = _clip;
		musicSource.Play();
	}
}
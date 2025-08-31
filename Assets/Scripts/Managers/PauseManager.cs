using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] GameObject pauseMenuUI;
	[SerializeField] AudioClip pauseSound;
	[SerializeField] AudioClip resumeSound;

	bool isPaused = false;

	void Awake()
	{
		Time.timeScale = 1f;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			PauseButton();
	}

	public void PauseButton()
	{
		if (isPaused)
		{
			SoundManager.Instance.Play(resumeSound);
			Resume();
		}
		else
		{
			SoundManager.Instance.Play(pauseSound);
			Pause();
		}
	}

	public void Resume()
	{
		if (Time.timeScale == 0f)
		{
			pauseMenuUI.SetActive(false);
			Time.timeScale = 1f;
			isPaused = false;
		}
	}

	public void Pause()
	{
		if (Time.timeScale == 1f)
		{
			pauseMenuUI.SetActive(true);
			Time.timeScale = 0f;
			isPaused = true;
		}
	}
}
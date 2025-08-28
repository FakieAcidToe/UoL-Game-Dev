using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] GameObject pauseMenuUI;
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
		if (isPaused) Resume();
		else Pause();
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}
}
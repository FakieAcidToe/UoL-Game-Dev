using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
	public void LoadLevelByIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}
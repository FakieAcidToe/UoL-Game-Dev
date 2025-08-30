using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        int point = GameObject.Find("Game Manager").GetComponent<PointManager>().GetPoint();
        GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>().AddUpgradePoints("GameOver", (int)(point * 0.5));
    }
}

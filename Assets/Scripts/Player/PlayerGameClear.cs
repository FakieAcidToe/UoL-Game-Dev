using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameClear : MonoBehaviour
{
    [SerializeField] GameObject gameClearUI;
    [SerializeField] int point;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        gameClearUI = canvas.Find("Game Clear Screen").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameClear()
    {
        Time.timeScale = 0f;
        gameClearUI.SetActive(true);
        point = GameObject.Find("Game Manager").GetComponent<PointManager>().GetPoint();
        GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>().AddUpgradePoints("GameClear", point);
    }
}

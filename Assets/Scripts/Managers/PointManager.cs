using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] float pointMultiplier;
    [SerializeField] Text pointText;

    // Start is called before the first frame update
    void Start()
    {
        pointMultiplier = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>().GetStat("POINT");
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = "Points : " + points;
    }

    public void AddPoint(int point)
    {
        points += (int)(point * pointMultiplier);
    }

    public int GetPoint()
    {
        return (int)(points *0.1);
    }
}

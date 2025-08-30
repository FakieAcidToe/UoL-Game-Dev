using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDrops : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] GameObject pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePoint()
    {
        pManager.GetComponent<PointManager>().AddPoint(points);
    }
}

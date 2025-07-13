using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;
    public int playerAtk = 10;
    public int atkUpgrades = 0;
    public int playerHP = 100;
    public int hpUpgrades = 0;
    public int playerCrit = 10;
    public int critUpgrades = 0;

    public int playerUpgradePoints = 1000;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateStat(string stat, int cost)
    {
        if (stat == "ATK")
        {
            if (atkUpgrades < 30 && playerUpgradePoints >= cost)
            {
                atkUpgrades++;
                playerAtk += (atkUpgrades) * 10;
                playerUpgradePoints -= cost;
            }

        }
        else if (stat == "HP" && playerUpgradePoints >= cost)
        {
            if (hpUpgrades < 30)
            {
                hpUpgrades++;
                playerHP += (hpUpgrades) * 100;
                playerUpgradePoints -= cost;
            }
        }
        else if (stat == "CRIT" && playerUpgradePoints >= cost)
        {
            if (critUpgrades < 15)
            {
                critUpgrades++;
                playerCrit += 5;
                playerUpgradePoints -= cost;
            }
        }
    }
}

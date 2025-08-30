using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	public static PlayerStatus Instance;
	public PlayerEquipment selectedCharacter;

	public float playerDMGB = 1;
	public int dmgbUpgrades = 0;

	public int playerHP = 100;
	public int hpUpgrades = 0;
	public int basePlayerHP = 100;

	public float playerCrit = 0.1f;
	public int critUpgrades = 0;

	public float playerSPD = 1;
	public int spdUpgrades = 0;

	public float playerPUR = 1;
	public int purUpgrades = 0;

	public float playerATKSPD = 1;

    internal void AddUpgradePoints()
    {
        throw new NotImplementedException();
    }

    public int atkspdUpgrades = 0;

	public float playerDMGR = 0;
	public int dmgrUpgrades = 0;

	public float playerCD = 0;
	public int cdUpgrades = 0;

	public float playerEXP = 1.00f; // note: affects experience required to level up rather than exp gained
	public int expUpgrades = 0;

	public float playerPoint = 1.00f; // note: not implemented yet
	public int pointUpgrades = 0;

	public int playerUpgradePoints = 10000;

	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public int GetUpgradePoints()
	{
		return playerUpgradePoints;
	}

	public int GetUpgrades(string stat)
	{
		switch (stat)
		{
			case "ATK":
				return dmgbUpgrades;
			case "HP":
				return hpUpgrades;
			case "CRIT":
				return critUpgrades;
			case "SPD":
				return spdUpgrades;
			case "PICKUP":
				return purUpgrades;
			case "ATKSPD":
				return atkspdUpgrades;
			case "DEF":
				return dmgrUpgrades;
			case "CD":
				return cdUpgrades;
			case "EXP":
				return expUpgrades;
			case "POINT":
				return pointUpgrades;
		}
		return 0;
	}

	public void UpdateStat(string stat, int cost, int maxLevel, float amount)
	{
		if (playerUpgradePoints >= cost)
		{
			switch (stat)
			{
				case "ATK":
					if (dmgbUpgrades < maxLevel)
					{
						dmgbUpgrades++;
						playerDMGB += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "HP":
					if (hpUpgrades < maxLevel)
					{
						hpUpgrades++;
						playerHP += (int)(basePlayerHP * (amount));
						playerUpgradePoints -= cost;
					}
					break;
				case "CRIT":
					if (critUpgrades < maxLevel)
					{
						critUpgrades++;
						playerCrit += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "SPD":
					if (spdUpgrades < maxLevel)
					{
						spdUpgrades++;
						playerSPD += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "PICKUP":
					if (purUpgrades < maxLevel)
					{
						purUpgrades++;
						playerPUR += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "ATKSPD":
					if (atkspdUpgrades < maxLevel)
					{
						atkspdUpgrades++;
						playerATKSPD += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "DEF":
					if (dmgrUpgrades < maxLevel)
					{
						dmgrUpgrades++;
						playerDMGR += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "CD":
					if (cdUpgrades < maxLevel)
					{
						cdUpgrades++;
						playerCD += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "EXP":
					if (expUpgrades < maxLevel)
					{
						expUpgrades++;
						playerEXP += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "POINT":
					if (pointUpgrades < maxLevel)
					{
						pointUpgrades++;
						playerPoint += amount;
						playerUpgradePoints -= cost;
					}
					break;
			}
		}
	}

	public void ResetUpgrades(int hpCost, int dmgbCost, int dmgrCost, int spdCost, int critCost, int purCost, int atkspdCost, int cdCost, int expCost, int pointCost)
	{
		int hpRefund = hpCost * (hpUpgrades * (hpUpgrades + 1)) / 2;
		int dmgbRefund = dmgbCost * (dmgbUpgrades * (dmgbUpgrades + 1)) / 2;
		int dmgrRefund = dmgrCost * (dmgrUpgrades * (dmgrUpgrades + 1)) / 2;
		int spdRefund = spdCost * (spdUpgrades * (spdUpgrades + 1)) / 2;
		int critRefund = critCost * (critUpgrades * (critUpgrades + 1)) / 2;
		int purRefund = purCost * (purUpgrades * (purUpgrades + 1)) / 2;
		int atkspdRefund = atkspdCost * (atkspdUpgrades * (atkspdUpgrades + 1)) / 2;
		int cdRefund = cdCost * (cdUpgrades * (cdUpgrades + 1)) / 2;
		int expRefund = expCost * (expUpgrades * (expUpgrades + 1)) / 2;
		int pointRefund = pointCost * (pointUpgrades * (pointUpgrades + 1)) / 2;

		playerUpgradePoints += hpRefund + dmgbRefund + dmgrRefund + spdRefund + critRefund + purRefund + atkspdRefund + cdRefund + expRefund + pointRefund;
		ResetStatToBase();
	}

	public void ResetStatToBase()
	{
		playerDMGB = 1;
		playerHP = basePlayerHP;
		playerCrit = 0.1f;
		playerSPD = 1;
		playerPUR = 1;
		playerATKSPD = 1;
		playerDMGR = 0;
		playerCD = 0;
		playerEXP = 1.00f;
		playerPoint = 1.00f;

		dmgbUpgrades = 0;
		hpUpgrades = 0;
		critUpgrades = 0;
		spdUpgrades = 0;
		purUpgrades = 0;
		atkspdUpgrades = 0;
		dmgrUpgrades = 0;
		cdUpgrades = 0;
		expUpgrades = 0;
		pointUpgrades = 0;
	}

	public float GetStat(string stat)
	{
		switch (stat)
		{
			case "POINT":
				return playerPoint;
			default:
				return 0;
		}
	}

	public void AddUpgradePoints(string state, int point)
	{
		if (state == "GameOver")
        {
			playerUpgradePoints += point;
        }
	}
}
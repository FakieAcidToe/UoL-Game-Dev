using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	public static PlayerStatus Instance;
	public PlayerEquipment selectedCharacter;

	public int playerAtk = 10; // note: not implemented yet (damage is calculated with integers)
	public int ATKUpgrades = 0;
	public int basePlayerAtk = 10;
	public int playerHP = 100;
	public int hpUpgrades= 0;
	public int basePlayerHP = 100;
	public int playerCrit = 10;
	public int critUpgrades = 0;

	public float playerSPD = 1;
	public int spdUpgrades = 0;

	public float playerPUR = 1;
	public int purUpgrades = 0;

	public float playerAS = 1;
	public int asUpgrades = 0;

	public int playerDef = 10; // note: not implemented yet (we dont currently have a defense system)
	public int defUpgrades = 0;
	public int basePlayerDef = 10;

	public float playerCD = 0;
	public int cdUpgrades = 0;

	public float playerEXP = 1.00f; // note: affects experience required to level up rather than exp gained
	public int expUpgrades = 0;

	public float playerPoint = 1.00f; // note: not implemented yet (idk what this is supposed to do)
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

	public void UpdateStat(string stat, int cost, int maxLevel, float amount)
	{
		if (playerUpgradePoints >= cost)
		{
			switch (stat)
			{
				case "ATK":
					if (ATKUpgrades < maxLevel)
					{
						ATKUpgrades++;
						playerAtk += (int)(basePlayerAtk * (amount));
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
						playerCrit += (int)amount;
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
					if (asUpgrades < maxLevel)
					{
						asUpgrades++;
						playerAS += amount;
						playerUpgradePoints -= cost;
					}
					break;
				case "DEF":
					if (defUpgrades < maxLevel)
					{
						defUpgrades++;
						playerDef += (int)(basePlayerDef * (amount));
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

	public int getUpgradePoints()
	{
		return playerUpgradePoints;
	}

	public int getUpgrades(string stat)
	{
		switch (stat)
		{
			case "ATK":
				return ATKUpgrades;
			case "HP":
				return hpUpgrades;
			case "CRIT":
				return critUpgrades;
			case "SPD":
				return spdUpgrades;
			case "PICKUP":
				return purUpgrades;
			case "ATKSPD":
				return asUpgrades;
			case "DEF":
				return defUpgrades;
			case "CD":
				return cdUpgrades;
			case "EXP":
				return expUpgrades;
			case "POINT":
				return pointUpgrades;
		}
		return 0;
	}
}
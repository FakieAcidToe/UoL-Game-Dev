using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public string upgradeName;
    public Text TextBox;
    public int upgradeLVL;
    public int upgradeCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (upgradeName)
        {
            case "ATK":
                upgradeLVL = PlayerStatus.Instance.atkUpgrades;
                break;
            case "HP":
                upgradeLVL = PlayerStatus.Instance.hpUpgrades;
                break;
            case "CRIT":
                upgradeLVL = PlayerStatus.Instance.critUpgrades;
                break;
        }
        upgradeCost = (upgradeLVL + 1) * 100;

        TextBox.text = string.Format("{0,-5}: Level {1} -> {2}   Cost: {3}", upgradeName, upgradeLVL, upgradeLVL + 1, upgradeCost);

    }

    public void UpgradeStat()
    {
        PlayerStatus.Instance.UpdateStat(upgradeName, upgradeCost);
    }
}
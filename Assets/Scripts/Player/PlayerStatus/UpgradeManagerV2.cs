using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManagerV2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string upgradeName;
    public int baseUpgradeCost;
    public int currentUpgradeCost;
    public int maxUpgradeCount;
    public float upgradeAmount;

    public Text displayTextBox;
    private Coroutine resetCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgradeStatus();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void UpgradeStat()
    {
        PlayerStatus.Instance.UpdateStat(upgradeName, currentUpgradeCost, maxUpgradeCount, upgradeAmount);
        UpdateUpgradeStatus();
        UpdateDisplay();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDisplay();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void UpdateDisplay()
    {
        if (displayTextBox != null)
        {
            switch (upgradeName)
            {
                case "HP":
                    displayTextBox.text = "HP UP \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "ATK":
                    displayTextBox.text = "ATK UP \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "CRIT":
                    displayTextBox.text = "CRIT UP \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "SPD":
                    displayTextBox.text = "MOVESPEED UP \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "PICKUP":
                    displayTextBox.text = "PICK UP RANGE \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "ATKSPD":
                    displayTextBox.text = "ATTACK SPEED \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "DEF":
                    displayTextBox.text = "DEF UP \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "CD":
                    displayTextBox.text = "SKILL COOLDOWN \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "EXP":
                    displayTextBox.text = "EXP GAIN \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
                case "POINT":
                    displayTextBox.text = "POINT GAIN \nQuantity " + (maxUpgradeCount - PlayerStatus.Instance.getUpgrades(upgradeName));
                    break;
            }

            displayTextBox.text += "\nCost " + currentUpgradeCost + "\nPoints: " + PlayerStatus.Instance.playerUpgradePoints;
        }
    }

    public void UpdateUpgradeStatus()
    {
        if (currentUpgradeCost < PlayerStatus.Instance.getUpgradePoints())
        {
            currentUpgradeCost = (PlayerStatus.Instance.getUpgrades(upgradeName) + 1) * baseUpgradeCost;
        }
    }
}
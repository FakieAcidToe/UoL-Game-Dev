using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManagerV2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string upgradeName;
    public int upgradeLVL;
    public int upgradeCost;

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
        PlayerStatus.Instance.UpdateStat(upgradeName, upgradeCost);
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
                case "ATK":
                    displayTextBox.text = "ATK UP \nQuantity " + (30 - upgradeLVL);
                    break;
                case "HP":
                    displayTextBox.text = "HP UP \nQuantity " + (30 - upgradeLVL);
                    break;
                case "CRIT":
                    displayTextBox.text = "CRIT UP \nQuantity " + (15 - upgradeLVL);
                    break;
            }

            displayTextBox.text += "\nCost " + upgradeCost + "\nPoints: " + PlayerStatus.Instance.playerUpgradePoints;
        }
    }

    public void UpdateUpgradeStatus()
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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private MapCube target;

    public void SetTarget(MapCube _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turrent.GetComponent<Turret>().turrentBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turrentBlueprint.GetSellAmount();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}

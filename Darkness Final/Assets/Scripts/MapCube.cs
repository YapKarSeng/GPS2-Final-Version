using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turrent;
    public TurrentBlueprint turrentBlueprint;
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (turrent != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        buildManager.BuildTurrentOn(this);
        turrentBlueprint = turrent.GetComponent<Turret>().turrentBlueprint;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turrentBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turrentBlueprint.upgradeCost;
        Destroy(turrent);

        GameObject _turret = (GameObject)Instantiate(turrentBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turrent = _turret;
        turrent.GetComponent<Turret>().selfMapCube = this;
        turrentBlueprint = turrent.GetComponent<Turret>().turrentBlueprint;
        isUpgraded = true;

    }

    public void SellTurret()
    {
        PlayerStats.Money += turrentBlueprint.GetSellAmount();

        Destroy(turrent);
        turrentBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;

        if (buildManager.afterBuild == false)
        {
            if (buildManager.HasMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = notEnoughMoneyColor;
            }
        }


    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

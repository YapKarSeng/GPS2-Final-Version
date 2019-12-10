using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager");
        }
        instance = this;
    }

    public GameObject standardTurrentPrefab;
    public GameObject anotherTurrentPrefab;
    public GameObject mirrorTurrentPrefab;

    private TurrentBlueprint turrentToBuild;
    private MapCube selectedTurret;
    public SelectionUI selectionUI;

    public bool afterBuild = false;

    public bool CanBuild { get { return turrentToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turrentToBuild.cost; } }

    public void BuildTurrentOn(MapCube mapCube)
    {

        if (afterBuild == false)
        {
            if (PlayerStats.Money < turrentToBuild.cost)
            {
                Debug.Log("No money");
                SoundManager.PlaySound("NoMoney");
                return;
            }

            PlayerStats.Money -= turrentToBuild.cost;

            GameObject turrent = (GameObject)Instantiate(turrentToBuild.prefab, mapCube.GetBuildPosition(), Quaternion.identity);
            mapCube.turrent = turrent;
            mapCube.turrent.GetComponent<Turret>().selfMapCube = mapCube;
            SoundManager.PlaySound("Build1");
        }
        afterBuild = true;

    }

    public void SelectNode(MapCube node)
    {
        if (selectedTurret == node)
        {
            DeselectNode();
            return;
        }

        selectedTurret = node;
        turrentToBuild = null;

        selectionUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedTurret = null;
        selectionUI.Hide();
    }

    public void SelectTurrentBuild(TurrentBlueprint turrent)
    {
        turrentToBuild = turrent;
        DeselectNode();
    }
}

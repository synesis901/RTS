using UnityEngine;
using System.Collections;

public class UnitCreationBasic : MonoBehaviour {

    bool selected;
    
    //menu stuff
    float menuWidth = 200;
    float menuHeight = 180;
    float buttonWidth = 100;
    float buttonHeight = 60;

    GameObject[] allTanks;


    float buildTimeStamp = -1;

    public GameObject selectionCircle;

    public enum BuildingType
    {
        Headquarters = 1,
        BuilderBarracks = 2,
        MeleeBarracks = 3,
        RangeBarracks = 4
    }

    float builderBuildTime = 30.0f;
    float meleeBuildTime = 60.0f;
    float rangeBuildTime = 60.0f;

    float meleeUpgradeTime = 45.0f;
    float rangeUpgradeTime = 45.0f;

    float buildTimeModifier = 0.0f;
    int upgradeModifier = 0;

    public BuildingType buildingDesignation;

    public GameObject builder;
    GameObject[] spawnLocations;

    bool buildingUnit;

    bool createUnit;

    public enum UpgradeOrBuilding
    {
        None = 0,
        Building = 1,
        Upgrading = 2
    }

    UpgradeOrBuilding upgradeOrBuilding = UpgradeOrBuilding.None;

	// Use this for initialization
	void Start () {
        selected = false;
        buildingUnit = false;
        createUnit = false;
	    
	}

    void OnGUI()
    {
        int count = 0;
        allTanks = GameObject.FindGameObjectsWithTag("Friendly");
        switch (buildingDesignation)
        {
            case BuildingType.MeleeBarracks:
                foreach (GameObject tank in allTanks)
                {
                    if (tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>().unitType == UnitAI.UnitType.Melee)
                    {
                        count++;
                    }

                }
                break;
            case BuildingType.RangeBarracks:
                foreach (GameObject tank in allTanks)
                {
                    if (tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>().unitType == UnitAI.UnitType.Range)
                    {
                        count++;
                    }

                }
                break;
            case BuildingType.Headquarters:
                foreach (GameObject tank in allTanks)
                {
                    if (tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>().unitType == UnitAI.UnitType.Builder)
                    {
                        count++;
                    }

                }
                break;
        }

        if (selected)
        {
            GUI.Box(new Rect(Screen.width - (menuWidth + 10), 10, menuWidth, menuHeight), buildingDesignation.ToString());

            switch (buildingDesignation)
            {
                case BuildingType.Headquarters:

                    if (!buildingUnit)
                    {
                        if (count < 3)
                        {
                            if (GUI.Button(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Build Unit"))
                            {
                                buildingUnit = true;
                                buildTimeStamp = Time.time;
                                buildTimeModifier = builderBuildTime;
                                upgradeOrBuilding = UpgradeOrBuilding.Building;
                            }
                        }
                    }
                    else
                    {
                        if(upgradeOrBuilding == UpgradeOrBuilding.Building)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Building... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                        else if(upgradeOrBuilding == UpgradeOrBuilding.Upgrading)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Upgrading... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                    }

                    break;
                case BuildingType.BuilderBarracks:
                    break;
                case BuildingType.MeleeBarracks:
                    if (!buildingUnit)
                    {
                        if (count < 3)
                        {
                            if (GUI.Button(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Build Unit"))
                            {
                                buildingUnit = true;
                                buildTimeStamp = Time.time;
                                buildTimeModifier = meleeBuildTime;
                                upgradeOrBuilding = UpgradeOrBuilding.Building;
                            }
                        }
                        if (GUI.Button(new Rect(Screen.width - (menuWidth - 10), 110, buttonWidth, buttonHeight), "Upgrade Attack \n Current : " + upgradeModifier))
                        {
                            buildingUnit = true;
                            buildTimeStamp = Time.time;
                            buildTimeModifier = meleeUpgradeTime;
                            upgradeOrBuilding = UpgradeOrBuilding.Upgrading;
                        }
                    }
                    else
                    {
                        if(upgradeOrBuilding == UpgradeOrBuilding.Building)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Building... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                        else if(upgradeOrBuilding == UpgradeOrBuilding.Upgrading)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Upgrading... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                    }
                    break;
                case BuildingType.RangeBarracks:
                    if (!buildingUnit && count < 3)
                    {
                        if (count < 3)
                        {
                            if (GUI.Button(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Build Unit"))
                            {
                                buildingUnit = true;
                                buildTimeStamp = Time.time;
                                buildTimeModifier = rangeBuildTime;
                                upgradeOrBuilding = UpgradeOrBuilding.Building;
                            }
                        }
                        if (GUI.Button(new Rect(Screen.width - (menuWidth - 10), 110, buttonWidth, buttonHeight), "Upgrade Attack"))
                        {
                            buildingUnit = true;
                            buildTimeStamp = Time.time;
                            buildTimeModifier = rangeUpgradeTime;
                            upgradeOrBuilding = UpgradeOrBuilding.Upgrading;
                        }
                    }
                    else
                    {
                        if(upgradeOrBuilding == UpgradeOrBuilding.Building)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Building... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                        else if(upgradeOrBuilding == UpgradeOrBuilding.Upgrading)
                            GUI.Label(new Rect(Screen.width - (menuWidth - 10), 40, buttonWidth, buttonHeight), "Upgrading... : " + Mathf.RoundToInt((buildTimeStamp + buildTimeModifier) - Time.time).ToString());
                    }
                    break;
            }
        }

        if (createUnit == true)
        {
            if (upgradeOrBuilding == UpgradeOrBuilding.Building)
            {
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
                foreach (GameObject spawnLocation in spawnLocations)
                {
                    if (spawnLocation.transform.parent == gameObject.transform && !spawnLocation.GetComponent<SpawnLocation>().GetOccupied())
                    {
                        spawnPosition = spawnLocation.transform.position;
                        spawnLocation.GetComponent<SpawnLocation>().SetOccupiedStatus(true);
                        break;
                    }
                }
                GameObject unitCreation = Instantiate(builder, spawnPosition, Quaternion.identity) as GameObject;
                
                switch (buildingDesignation)
                {
                    case BuildingType.Headquarters:
                        unitCreation.GetComponent<UnitAI>().SetUnitType(UnitAI.UnitType.Builder);
                        unitCreation.GetComponent<UnitAI>().SetRangeUpgradeModifer(upgradeModifier);
                        break;
                    case BuildingType.MeleeBarracks:
                        unitCreation.GetComponent<UnitAI>().SetUnitType(UnitAI.UnitType.Melee);
                        unitCreation.GetComponent<UnitAI>().SetRangeUpgradeModifer(upgradeModifier);
                        break;
                    case BuildingType.RangeBarracks:
                        unitCreation.GetComponent<UnitAI>().SetUnitType(UnitAI.UnitType.Range);
                        unitCreation.GetComponent<UnitAI>().SetRangeUpgradeModifer(upgradeModifier);
                        break;
                }
            }

            if (upgradeOrBuilding == UpgradeOrBuilding.Upgrading)
            {
                upgradeModifier++;
                switch (buildingDesignation)
                {
                    case BuildingType.MeleeBarracks:
                        foreach (GameObject tank in allTanks)
                        {
                            if (tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>().unitType == UnitAI.UnitType.Melee)
                            {
                                tank.GetComponent<UnitAI>().SetRangeUpgradeModifer(upgradeModifier);
                            }

                        }
                        break;
                    case BuildingType.RangeBarracks:
                        foreach (GameObject tank in allTanks)
                        {
                            if (tank.GetComponent<UnitAI>() != null && tank.GetComponent<UnitAI>().unitType == UnitAI.UnitType.Range)
                            {
                                tank.GetComponent<UnitAI>().SetRangeUpgradeModifer(upgradeModifier);
                            }

                        }
                        break;
                }
            }

            upgradeOrBuilding = UpgradeOrBuilding.None;
            createUnit = false;
        }

        if (Time.time > buildTimeStamp + buildTimeModifier && createUnit == false && buildingUnit == true)
        {
            buildingUnit = false;
            createUnit = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectBuilding(bool selection)
    {
        selected = selection;

        //selection circle logic
        if (selected)
        {
            selectionCircle.SetActive(true);
        }
        else
        {
            selectionCircle.SetActive(false);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseSelectionRaycast : MonoBehaviour {

    RaycastHit hit;
    public List<Collider> selectedList;
    public List<NavMeshAgent> agentList;
    UnitAI.TankInformation tankInformation;

    bool selected;

	// Use this for initialization
	void Start () {
        selectedList = new List<Collider>();
        agentList = new List<NavMeshAgent>();
	}

    void OnGUI()
    {
        int nextLine = 0;
        int nextLabel = 0;
        GUI.Box(new Rect(0, Screen.height - 120, Screen.width, 120), " ");
        for (int i = 0; i < selectedList.Count; i++)
        {
            if (selectedList[i].GetComponent<UnitAI>() != null)
            {
                tankInformation = selectedList[i].GetComponent<UnitAI>().GetTankInformation();
                GUI.Box(new Rect(10 + nextLabel * 160, Screen.height - (110 - (55 * nextLine)), 150, 50), selectedList[i].GetComponent<UnitAI>().unitType.ToString());
                GUI.Label(new Rect(15 + nextLabel * 160, Screen.height - (95 - (55 * nextLine)), 95, 25), "Health: " + tankInformation.tankHealth + "/" + tankInformation.maxHealth);
                GUI.Label(new Rect(15 + nextLabel * 160, Screen.height - (80 - (55 * nextLine)), 95, 25), "Damage: " + tankInformation.baseDamage + " + " + tankInformation.tankUpgradeDamage);

                nextLabel++;
                
                if (i == 4)
                {
                    nextLabel = 0;
                    nextLine++;
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        Ray rayMousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, rayMousePosition.direction, out hit, 1000))
            {
                if (hit.collider.tag == "Friendly")
                {
                    selected = false;

                    for (int i = 0; i < selectedList.Count && !selected; i++)
                    {
                        if (hit.collider == selectedList[i])
                        {
                            selected = true;
                        }
                    }

                    if (!selected)
                    {
                        if (!Input.GetKey(KeyCode.LeftShift))
                        {
                            DeselectAll();
                        }
                        selectedList.Add(hit.collider);

                        if (selectedList[selectedList.Count - 1].GetComponent<UnitCreationBasic>() != null)
                        {
                            DeselectAll();
                            selectedList.Add(hit.collider);
                            selectedList[selectedList.Count - 1].GetComponent<UnitCreationBasic>().SendMessage("SelectBuilding", true);
                        }

                        if (selectedList[selectedList.Count - 1].GetComponent<UnitAI>() != null)
                        {
                            //so not to select building and units at the same time
                            for (int i = 0; i < selectedList.Count; i++)
                            {
                                if (selectedList[i].GetComponent<UnitCreationBasic>() != null)
                                {
                                    selectedList[i].GetComponent<UnitCreationBasic>().SendMessage("SelectBuilding", false);
                                    selectedList.RemoveAt(i);
                                }
                            }

                            agentList.Add(hit.transform.GetComponent<NavMeshAgent>());
                            selectedList[selectedList.Count - 1].GetComponent<UnitAI>().SendMessage("SelectUnit", true);

                        }
                    }
                }
                else if ((hit.collider.tag == "Enemy")||(hit.collider.tag == "EnemyBuilding"))
                {
                    if (selectedList.Count > 0)
                    {
                        //hit.transform.SendMessage("SelectTank", true);
                        //target = hit.collider;

                        for (int i = 0; i < agentList.Count; i++)
                        {
                            if (agentList[i].GetComponent<UnitAI>() != null)
                            {
                                // Set the destination of the selected units NavMeshAgent
                                agentList[i].destination = hit.point;

                                selectedList[i].transform.SendMessage("AssignTarget", hit.collider);
                            }
                        }
                    }
                }
                else if(hit.transform.tag.Equals("Floor"))
                {
                    //Debug.Log("Hit");
                    // If a friendly unit is selected
                    if (agentList.Count > 0)
                    {
                        for (int i = 0; i < agentList.Count; i++)
                        {
                            //modifier for more than 3 tanks
                            //int tankDistanceModifier = 1;
                            // Set the destination of the selected units NavMeshAgent
                            agentList[i].stoppingDistance = 0.0f;

                            ////this is for non colision at the destination.  Have to ignore 1st point, reason for if/else
                            //if (i > 0)
                            //{
                            //    agentList[i].destination = hit.point + new Vector3(tankDistance * tankDistanceModifier * Mathf.Cos(Mathf.PI * i), 0, 0);
                            //    //adding modifier to multiply distance in the following manner 1-1-2-2-3-3-...
                            //    if (Mathf.Cos(Mathf.PI * i) > 0)
                            //        tankDistanceModifier++;
                            //}
                            //else
                                agentList[i].destination = hit.point;

                            //selectedList[i].transform.SendMessage("ClearTarget");
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectAll();
        }
	
	}

    void DeselectAll()
    {
        // Deselect all tanks currently selected.
        if (selectedList.Count > 0)
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i].GetComponent<UnitCreationBasic>() != null)
                {
                    selectedList[i].GetComponent<UnitCreationBasic>().SendMessage("SelectBuilding", false);
                }

                if (selectedList[i].GetComponent<UnitAI>() != null)
                {
                    selectedList[i].GetComponent<UnitAI>().SendMessage("SelectUnit", false);
                }
            }
        }

        // Clear both lists.
        selectedList.Clear();
        agentList.Clear();
    }
}

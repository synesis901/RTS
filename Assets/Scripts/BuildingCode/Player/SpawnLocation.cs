using UnityEngine;
using System.Collections;

public class SpawnLocation : MonoBehaviour {

    bool occupied = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if(Physics.CheckSphere(new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)), 3))
        //    transform.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
	}

    //void OnTriggerEnter(Collider unit)
    //{
    //    if (unit.tag.Equals("Friendly"))
    //    {
    //        Debug.Log(transform.position);
    //        transform.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    //    }
    //}


    public bool GetOccupied()
    {
        return occupied;
    }

    public void SetOccupiedStatus(bool status)
    {
        occupied = status;
    }

}

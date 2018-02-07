using UnityEngine;
using System.Collections;

public class EnemyBuildingBasic : MonoBehaviour {

    bool selected;

    int health = 100;

    public GameObject selectionCircle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
        {
            Destroy(gameObject);
        }
	
	}

    public void SelectUnit(bool selection)
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

    void OnCollisionEnter(Collision collisionObject)
    {
        //Debug.Log(collisionObject.transform.tag);
        if (collisionObject.transform.tag.Equals("Projectile"))
        {
            health -= collisionObject.gameObject.GetComponent<Projectile>().GetDamage();
            if (health < 0)
                health = 0;
        }
    }

}

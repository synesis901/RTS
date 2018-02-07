using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public GameObject explosion;
    private int projectileDamage = 10;

    public void SetDamage(int damage = 10)
    {
        projectileDamage = damage;
    }

    public int GetDamage()
    {
        return projectileDamage;
    }
	
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
	
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Instantiate(explosion, contact.point, rotation);

		Destroy(gameObject);
	}
}

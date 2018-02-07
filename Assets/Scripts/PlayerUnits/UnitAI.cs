using UnityEngine;
using System.Collections;

public class UnitAI : MonoBehaviour {


    #region projectile information

    public Rigidbody projectile;
    public float projectileVelocity = 50.0f;
    
    float lastFire = -1.0f;
    public NavMeshAgent agent;
    #endregion

    #region teamType
    public enum UnitType
    {
        Builder = 1,
        Melee = 2,
        Range = 3
    }
    #endregion

    #region Tank Stats

    private int maxHealth = 100;
    public  int tankHealth = 100;
    public float tankShotDelay = 1.0f;

    private int tankShootDamageUpgrade = 0;
    private int meleeDamageUpgrade = 0;
    private int baseDamage = 10;
    private int damage = 0;

    public struct TankInformation
    {
        public int maxHealth;
        public int tankHealth;
        public float tankShootDelay;
        public int baseDamage;
        public int tankUpgradeDamage;

        public void Init(int maxH, int tankH, float tankSD, int baseDMG, int upgradeDMG)
        {
            maxHealth = maxH;
            tankHealth = tankH;
            tankShootDelay = tankSD;
            baseDamage = baseDMG;
            tankUpgradeDamage = upgradeDMG;
        }

        public void SetModifiableVaribles(int tankH, int upgradeDMG)
        {
            tankUpgradeDamage = upgradeDMG;
            tankHealth = tankH;
        }
    }

    #endregion

    #region tank selection information

    bool selected;
    Collider target;
    public GameObject selectionCircle;

    #endregion

    public UnitType unitType;

    bool chargeUpgrade = false;
    private Transform doNotEnterObject;

    TankInformation tankInformation;

    public TankInformation GetTankInformation()
    {
        tankInformation.SetModifiableVaribles(tankHealth, tankShootDamageUpgrade);
        return tankInformation;
    }

    public void SetRangeUpgradeModifer(int upgradeModifer)
    {
        tankShootDamageUpgrade = upgradeModifer;
        damage = baseDamage + tankShootDamageUpgrade;
    }

	// Use this for initialization
	void Start () {
        selected = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10.0f;
        damage = baseDamage + tankShootDamageUpgrade;
        tankInformation.Init(maxHealth, tankHealth, tankShotDelay, baseDamage, tankShootDamageUpgrade);
	}
	
	// Update is called once per frame
	void Update () {
        switch (unitType)
        {
            case UnitType.Builder:
                break;
            case UnitType.Range:
                
                if (target != null)
                {
                    agent.stoppingDistance = 45.0f;
                    //Debug.Log(Vector3.Distance(this.transform.position, target.transform.position));
                    if (Vector3.Distance(this.transform.position, target.transform.position) < 60.0)
                    {

                        //look towards the enemy
                        Vector3 targetDir = target.transform.position - transform.position;
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0F);
                        transform.rotation = Quaternion.LookRotation(newDir);

                        //delay shooting function
                        if (lastFire + tankShotDelay < Time.time)
                        {
                            lastFire = Time.time;

                            // Instantiate a copy of the projectile
                            Rigidbody newProjectile = (Rigidbody)Instantiate(projectile, transform.Find("ProjectileLocation").transform.position, transform.rotation);
                            newProjectile.GetComponent<Projectile>().SetDamage(damage);

                            // Give the projectile a forward velocity
                            newProjectile.velocity = transform.TransformDirection(0, 0, projectileVelocity);
                        }
                    }
                }
                break;
            case UnitType.Melee:
                
                if (target != null)
                {
                    agent.stoppingDistance = 10.0f;
                    if(target.tag.Equals("EnemyBuilding"))
                    {
                        if (Vector3.Distance(this.transform.position, target.transform.position) < 30.0f)
                        {
                            //look towards the enemy
                            Vector3 targetDir = target.transform.position - transform.position;
                            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0F);
                            transform.rotation = Quaternion.LookRotation(newDir);

                            //delay shooting function
                            {
                                if (lastFire + tankShotDelay < Time.time)
                                {
                                    lastFire = Time.time;

                                    // Instantiate a copy of the projectile
                                    Rigidbody newProjectile = (Rigidbody)Instantiate(projectile, transform.Find("ProjectileLocation").transform.position, transform.rotation);
                                    newProjectile.GetComponent<Projectile>().SetDamage(damage);

                                    // Give the projectile a forward velocity
                                    newProjectile.velocity = transform.TransformDirection(0, 0, projectileVelocity);
                                }
                            }
                        }
                    }
                    else if (target.tag.Equals("Enemy"))
                    {
                        if (Vector3.Distance(this.transform.position, target.transform.position) < 10.0f)
                        {
                            //look towards the enemy
                            Vector3 targetDir = target.transform.position - transform.position;
                            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0F);
                            transform.rotation = Quaternion.LookRotation(newDir);

                            //delay shooting function
                            {
                                if (lastFire + tankShotDelay < Time.time)
                                {
                                    lastFire = Time.time;

                                    // Instantiate a copy of the projectile
                                    Rigidbody newProjectile = (Rigidbody)Instantiate(projectile, transform.Find("ProjectileLocation").transform.position, transform.rotation);
                                    newProjectile.GetComponent<Projectile>().SetDamage(damage);

                                    // Give the projectile a forward velocity
                                    newProjectile.velocity = transform.TransformDirection(0, 0, projectileVelocity);
                                }
                            }
                        }
                    }
                }
                else if (doNotEnterObject != null)
                    doNotEnterObject = null;
                break;
        }

        if (tankHealth <= 0)
        {
            Destroy(gameObject);
        }
	
	}

    public void SetUnitType(UnitType unitDesignation)
    {
        unitType = unitDesignation;
    }

    public void SelectUnit(bool selection)
    {
        selected = selection;

        //selection circle logic
        if (selected)
        {
            selectionCircle.SetActive(true);
            // If I have a target then activate my target's selection circle.
            if (target != null)
            {
                target.SendMessage("SelectUnit", true);
            }
        }
        else
        {
            // If I have a target deselect my target's selection circle.
            if (target != null)
            {
                target.SendMessage("SelectUnit", false);
            }
            selectionCircle.SetActive(false);
        }
    }

    public void AssignTarget(Collider newTarget)
    {
        // Assign me a target and activate my target's selection circle.
        target = newTarget;
        target.SendMessage("SelectUnit", true);
    }

    public void ClearTarget()
    {
        // Turn off my target's selection circle and set my target to null.
        if (target != null)
        {
            target.SendMessage("SelectUnit", false);
            target = null;
        }
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.transform.tag.Equals("Projectile"))
        {
            tankHealth -= collisionObject.gameObject.GetComponent<Projectile>().GetDamage();
            if (tankHealth < 0)
                tankHealth = 0;
        }
    }

    //void OnTriggerEnter(Collider collisionObject)
    //{
    //    Debug.Log(collisionObject.transform.tag.ToString());
    //    if (collisionObject.transform.tag.Equals("Enemy"))
    //    {
    //        Debug.Log(transform.position);
    //        doNotEnterObject = transform;
    //    }
    //}
}

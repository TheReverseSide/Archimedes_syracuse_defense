using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class Turret : MonoBehaviour
{
    #region Variables
    Transform target; //Selected target
    Enemy targetEnemy;
    string enemyToFocusOn = "";
    public bool isStayLockEngaged;
    bool manualFireControl;
    float seconds;
    float timer;

    public bool electricActivated;

    public bool isLaser;
    public bool isGun;
    public bool isCannon;
    public bool isRocket;

    public Node node;
    public GameObject[] enemies;
    public List<GameObject> passedUpEnemies;

    [HideInInspector]
    public Bullet bullet;
    [HideInInspector]
    public GameObject bulletGO;

    [Header("Card Filler - Odin multiline")]
    public string turretName;
    [MultiLineProperty(3)]
    public string turretDescription;
    public string upgradeStatus;
    public Sprite turretImage;
    public float bulletDamage;

    [Header("General")]
    public float range = 15f;
    public bool isLocked;

    [Header("Upgrade stuff")]
    [Header("Titles")]
    public string branch1Title;
    public string branch2Title;
    public string branch3Title;

    [Header("Upgrade names")]
    public string b1_u1_name;
    public bool hasB1UI;
    public string b1_u2_name;
    public bool hasB1U2;
    public string b1_u3_name;
    public bool hasB1U3;

    public string b2_u1_name;
    public bool hasB2UI;
    public string b2_u2_name;
    public bool hasB2U2;
    public string b2_u3_name;
    public bool hasB2U3;

    public string b3_u1_name;
    public bool hasB3UI;
    public string b3_u2_name;
    public bool hasB3U2;
    public string b3_u3_name;
    public bool hasB3U3;

    [Header("Use ammunition (Default)")]
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use laser")]
    [ToggleLeft]
    public bool useLaser;
    public LineRenderer lineRender;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float damageOverTime = 20f; //damage per second

    //[MinMaxSlider(0f, 1f, true)]
    public float slowPercent = .5f;

    public Vector3 laserOffset = new Vector3(0, 0f, 0);

    [Header("Unity setup fields -  Do not alter")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 8f;
    public Transform partToRotate;
    public Transform firePoint;
    public Transform firePointTwo;
    public Transform firePointThree;
    public Transform firePointFour;
    public Transform firePointFive;
    public Transform firePointSix;
    public Transform firePointSeven;
    public Transform firePointEight;
    public Transform firePointNine;
    public Transform firePointTen;
    public Transform firePointEleven;
    public Transform firePointTwelve;
    public Transform firePointThirteen;
    #endregion Variables

    void Start()
    {
        if (gameObject.CompareTag("CannonTurret_FireRate") || gameObject.CompareTag("GunTurret_FireRate") || gameObject.CompareTag("RocketTurret_Burst") || gameObject.CompareTag("RocketTurret_Splash") || gameObject.CompareTag("RocketTurret_Swarm"))
        { //add other conditions later
            manualFireControl = true;
        }

        InvokeRepeating("UpdateTarget", 0f, 0.5f); //repeats named method after 0s wait, repeats every .3 second
        isStayLockEngaged = false;

        bulletDamage = projectilePrefab.GetComponent<Bullet>().damageDefault;
    }

    void Update()
    {
        timer -= Time.fixedDeltaTime;

        if (target == null) //If there is no target, do nothing
        {
            if (gameObject.CompareTag("GunTurret_FireRate")) //Needs to shoot out of the barrel that happens to be on top
            {
                this.GetComponent<Animation>().Stop("GunTurretRecoil_FireRate"); //I need to stop the gatling gun anim somehwere
            }

            if (useLaser)
            {
                if (lineRender.enabled)
                {
                    lineRender.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        else
        {
            LockOnTarget();

            if (useLaser) Laser();

            else
            {
                if (fireCountdown <= 0f)
                {
                    //Filter according to turret and select appropriate fire mode
                    //if (gameObject.name.Contains("Mortar"))
                    //{
                    //    ShootMortar();
                    //}


                    ShootGunCannon();

                    fireCountdown = 1f / fireRate;
                    //if (!manualFireControl)
                    //{
                    //    fireCountdown = 1f / fireRate;
                    //}
                }
                if (!manualFireControl)
                {
                    ReduceFireCountDown();
                }
            }
        }
    }

    #region FIRING
    void ReduceFireCountDown()
    {
        fireCountdown -= Time.deltaTime;
    }

    void Laser()
    {
        //O_ this also needs to sort for enemy type, etc
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); //Very taxing, should not be in update. Instead cache into variable
        //targetEnemy.Slow(slowPercent);

        if (gameObject.CompareTag("LaserTurret1") || (gameObject.CompareTag("LaserTurret2")))
        {
            AudioManager.instance.Play("Laser1_2"); //audiomanager.instance ?
        }
        else if (gameObject.CompareTag("Laser3"))
        {
            AudioManager.instance.Play("Laser3");
        }

        if (!lineRender.enabled)
        {
            lineRender.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRender.SetPosition(0, firePoint.position);
        lineRender.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position; //Direction that points back towards our turrent for the particle effects

        impactEffect.transform.position = target.position + laserOffset;  //^Normalized so it is one unit long (???)  + dir.normalized
        impactEffect.transform.rotation = Quaternion.LookRotation(dir); //Changes rotation to the direction that we just established (towards turret)
    }

    void ShootGunCannon()
    {
        //Fires out of primary firePoint no matter what
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        if (electricActivated)
        {
            Debug.Log("electric activated");

            int number = Random.Range(1, 5);

            if (number == 1)
            {
                //Add electric effect - call it
                ElectricGlial electricGlial = new ElectricGlial();
                bullet = electricGlial.ElectrifyEffect(bullet);
            }
        }

        #region audio and anim Sorting
        if (gameObject.tag.Contains("cannon") || gameObject.tag.Contains("Cannon"))
        {
            //UPDATED
            if (gameObject.CompareTag("CannonTurret_Base")) //Nothing special
            {
                //AudioManager.instance.Play("Cannon1_2");
                this.GetComponent<Animation>().Play("CannonTurretRecoil_Base");
                BulletSeek(bullet);

                //FP2
                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
                BulletSeek(bullet);
            }
            else if (gameObject.CompareTag("CannonTurret_Mortar")) //Nothing special
            {
                //AudioManager.instance.Play("Cannon3");
                this.GetComponent<Animation>().Play("CannonTurretRecoil_HeavyMortar"); //Nothing special
                BulletSeek(bullet);
            }
            else if (gameObject.CompareTag("CannonTurret_LongRange"))
            {
                //AudioManager.instance.Play("Cannon3");
                this.GetComponent<Animation>().Play("CannonTurretRecoil_LongRange"); //Nothing special
                BulletSeek(bullet);

                //FP2
                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
                BulletSeek(bullet);
            }
            else if (gameObject.CompareTag("CannonTurret_FireRate"))
            {
                manualFireControl = true;
                StartCoroutine(CannonTurretFR());
            }
        }

        else if (gameObject.tag.Contains("gun") || gameObject.tag.Contains("Gun"))
        {
            if (gameObject.CompareTag("GunTurret_Base")) //Nothing special
            {
                //AudioManager.instance.Play("GunTurret1_2");
                this.GetComponent<Animation>().Play("GunTurretRecoil_Base");
                BulletSeek(bullet);

                //FP2
                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
                BulletSeek(bullet);
            }
            else if (gameObject.CompareTag("GunTurret_Sniper")) //Nothing special
            {
                //AudioManager.instance.Play("GunTurret3");
                this.GetComponent<Animation>().Play("GunTurretRecoil_Sniper");
                BulletSeek(bullet);
            }
            else if (gameObject.CompareTag("GunTurret_FireRate")) //Needs to shoot out of the barrel that happens to be on top
            {
                StartCoroutine(GunTurretGatling());
            }
            else if (gameObject.CompareTag("GunTurret_Power")) //Needs to shoot three, one with a different effect?
            {
                //AudioManager.instance.Play("GunTurret3");
                this.GetComponent<Animation>().Play("GunTurretRecoil_Power");

                //This is the main cannon
                //Add effect to bullet
                BulletSeek(bullet);

                //Normal bullets
                //FP2
                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
                BulletSeek(bullet);

                //FP3
                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointThree.position, firePointThree.rotation);
                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
                BulletSeek(bullet);
            }
        }

        else if (gameObject.tag.Contains("rocket") || gameObject.tag.Contains("Rocket"))
        {
            if (gameObject.CompareTag("RocketTurret_Base")) //Nothing special
            {
                BulletSeek(bullet);
                //AudioManager.instance.Play("RocketLaunch1_2");
            }
            else if (gameObject.CompareTag("RocketTurret_Burst")) //It should shoot a few rockets with a relatively small distance inbetween (Change firing points)
            {
                StartCoroutine(RocketBurst());
                //AudioManager.instance.Play("RocketLaunch3");
            }
            else if (gameObject.CompareTag("RocketTurret_Swarm")) //SHould shoot a bunch from large distances, should be inaccurate, can tweak Seek? What about close range? Devastating?
            {
                StartCoroutine(RocketSwarm());
                //AudioManager.instance.Play("RocketLaunch3");
            }
            else if (gameObject.CompareTag("RocketTurret_Splash")) //A few rockets should burst into several pieces upon (or before) impact
            {
                StartCoroutine(RocketSplash());
                //AudioManager.instance.Play("RocketLaunch3");
            }
        }
        #endregion

        #region checking for additional firepoints and shooting from them
        //if (firePointTwo != null)
        //{
        //    bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
        //    bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //    if (bullet != null)
        //    {
        //        bullet.Seek(target);
        //    }

        //    if (firePointThree != null)
        //    {
        //        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointThree.position, firePointThree.rotation);
        //        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //        if (bullet != null)
        //        {
        //            bullet.Seek(target);
        //        }

        //        if (firePointFour != null)
        //        {
        //            bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFour.position, firePointFour.rotation);
        //            bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //            if (bullet != null)
        //            {
        //                bullet.Seek(target);
        //            }

        //            if (firePointFive != null)
        //            {
        //                bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFive.position, firePointFive.rotation);
        //                bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //                if (bullet != null)
        //                {
        //                    bullet.Seek(target);
        //                }

        //                if (firePointSix != null)
        //                {
        //                    bulletGO = (GameObject)Instantiate(projectilePrefab, firePointSix.position, firePointSix.rotation);
        //                    bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //                    if (bullet != null)
        //                    {
        //                        bullet.Seek(target);
        //                    }

        //                    if (firePointSeven != null)
        //                    {
        //                        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointSeven.position, firePointSeven.rotation);
        //                        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //                        if (bullet != null)
        //                        {
        //                            bullet.Seek(target);
        //                        }

        //                        if (firePointEight != null)
        //                        {
        //                            bulletGO = (GameObject)Instantiate(projectilePrefab, firePointEight.position, firePointEight.rotation);
        //                            bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        //                            if (bullet != null)
        //                            {
        //                                bullet.Seek(target);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        #endregion
    }

    #region cannonTurret
    IEnumerator CannonTurretFR()
    {
        //seconds = 10f;

        //AudioManager.instance.Play("Cannon3");
        //this.GetComponent<Animation>().Play("CannonTurretRecoil_FireRate");

        BulletSeek(bullet);

        Debug.Log("firing 1 and 4");
        //FP2
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(1f);

        Debug.Log("firing 2 and 3");
        //FP4
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFour.position, firePointFour.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        //FP3
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointThree.position, firePointThree.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(1f);
        fireCountdown = 0;
    }
    #endregion

    #region Gatling gun
    IEnumerator GunTurretGatling()
    {
        seconds = .25f;
        this.GetComponent<Animation>().Play("GunTurretRecoil_FireRate");
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(seconds);
        Debug.Log(Time.deltaTime);

        //FP2
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(seconds);
        Debug.Log(Time.deltaTime);

        //FP4
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFour.position, firePointFour.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(seconds);
        Debug.Log(Time.deltaTime);

        //FP3
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointThree.position, firePointThree.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(seconds);
    }
    #endregion

    #region Rocket burst
    IEnumerator RocketBurst()
    {
        Debug.Log("burst 1");
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        seconds = Random.Range(.1f, .4f);
        yield return new WaitForSeconds(seconds);

        //FP2
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointTwo.position, firePointTwo.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        seconds = Random.Range(.1f, .4f);
        yield return new WaitForSeconds(seconds);

        //FP4
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFour.position, firePointFour.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        yield return new WaitForSeconds(1f);
        Debug.Log("burst 2");

        //FP3
        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointThree.position, firePointThree.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        BulletSeek(bullet);
        //AudioManager.instance.Play("GunTurret3");

        seconds = Random.Range(.1f, .4f);
        yield return new WaitForSeconds(seconds);


        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFour.position, firePointFour.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        bullet.Seek(target);
        //AudioManager.instance.Play("GunTurret3");

        seconds = Random.Range(.1f, .4f);
        yield return new WaitForSeconds(seconds);

        bulletGO = (GameObject)Instantiate(projectilePrefab, firePointFive.position, firePointFive.rotation);
        bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object
        bullet.Seek(target);
        //AudioManager.instance.Play("GunTurret3");

        Debug.Log("resetting burst");
        yield return new WaitForSeconds(1f);
        fireCountdown = 0;
    }
    #endregion

    IEnumerator RocketSplash()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator RocketSwarm()
    {//SHould fire within a certain range?
        //Should be accurate / inaccurate depending on range?
        yield return new WaitForSeconds(1f);
    }

    void ShootMissile()
    { //iterate through firing points, shooting one at a time - can there be a pause after all are empty?
      //ArrayList firePoints = new ArrayList();
        if (gameObject.tag.Contains("missile") || gameObject.tag.Contains("Missile"))
        //CompareTag("MissileLauncher1") || gameObject.CompareTag("MissileLauncher2") || gameObject.CompareTag("MissileLauncher3"))
        {
            AudioManager.instance.Play("MissileLaunchFlying"); //Should loop while it hasnt crashed into anything
        }

        //Add explode audio
    }

    void ShootMortar()
    {
        GameObject bulletGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>(); //grabs bullet script from game object

        bullet.ParabolaArc(bullet.transform, target.transform);

        if (this.firePointTwo != null)
        {
            //bullet.ParabolaArc(this.firePointTwo.transform, target); Update
        }

        //Audio
        if (gameObject.CompareTag("MortarTurret1") || gameObject.CompareTag("MortarTurret2"))
        {
            //AudioManager.instance.Play("MortarShoot1_2"); 
        }
        else if (gameObject.CompareTag("MortarTurret3"))
        {
            //AudioManager.instance.Play("MortarShoot3");
        }
    }
    #endregion

    #region TARGETING
    void LockOnTarget()
    {
        //Get a vector that points in the direction of closestEnemy(target)
        Vector3 dir = target.position - transform.position; //Point B - point A for distance calcuations

        Quaternion lookRotation = Quaternion.LookRotation(dir); //create a quaternion that sets our look rotation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //^Convert quaternion to euler angles(x,y,x) (what unity uses in rotation)
        //^Quaternion A is where we want to rotate from, B being where we want to rotate to, time.delta so it goes as time goes on

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //set euler angle for y to part
    }

    void BulletSeek(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void UpdateTarget()
    {
        //distance checks and searching through all objects takes computer power, so it is better to have it run twice a second or so 
        //renewed search through all objects tagged as enemy, finds closest, checks if its within range, then sets target 

        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        passedUpEnemies = new List<GameObject>();

        float closestDistance = Mathf.Infinity; //Using infinity so we dont accidentally make it too big

        if (isStayLockEngaged == true && target != null && (DistanceToEnemy(this.transform.position, target.transform.position)) <= range) //if staylocked is on and there is currently a target and they are within range, keep it that way
        {
            return;
        }

        //Otherwise clear enemy and re-evalutate closest target
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            bool isAddedToList = false;
            float distanceToEnemy = DistanceToEnemy(this.transform.position, enemy.transform.position);
            //Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                if (enemyToFocusOn.Equals(""))
                { //If the enmy to focus on is empty, do nothing special
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
                else //If there is, only allow those enemies to be targeted
                {
                    foreach (Transform child in enemy.transform) //Cycles through children looking for tag
                    {
                        if (child.tag == enemyToFocusOn)
                        {
                            closestDistance = distanceToEnemy;
                            closestEnemy = enemy;
                        }
                        else if (!isAddedToList)
                        {
                            passedUpEnemies.Add(enemy); //This may add duplicates because there are multiple children
                            isAddedToList = true;
                        }
                    }
                }
            }
        }

        if (closestEnemy == null && !enemyToFocusOn.Equals(""))
        {
            //Maybe serialize this to see how many enemies get added
            foreach (GameObject enemy in passedUpEnemies)
            {
                float distanceToEnemy = DistanceToEnemy(this.transform.position, enemy.transform.position);
                //Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null && closestDistance <= range) //If there is an enemy and it is within range...
        { //Add a ocondition here that fucntions as stay locked
            target = closestEnemy.transform; //..set it to the target
            targetEnemy = closestEnemy.GetComponent<Enemy>(); //sets targetEnemy to closest enemy and adds functionality from Enemy class (for takeDamage)
        }
        else { target = null; } //otherwise dont set a target
    }

    private float DistanceToEnemy(Vector3 thisTurret, Vector3 enemy)
    {
        return Vector3.Distance(thisTurret, enemy);
    }

    private void OnDrawGizmosSelected() //Display range of turrent in scene
    {
        Gizmos.color = Color.red; //Setting color
        Gizmos.DrawWireSphere(transform.position, range); //Draws a wire frame segun range
    }

    public GameObject returnProjectile()
    {
        if (projectilePrefab != null)
        { //!this.CompareTag("Laser1_2") && (!this.CompareTag("Laser3"))
            return projectilePrefab;
        }
        else return null;
    }

    public void setProjectile()
    {

    }

    public void EngageStayLock()
    {
        Debug.Log("changed to locked on");
        isStayLockEngaged = true;
    }

    public void EngageFreeLock()
    {
        Debug.Log("changed to free fire");
        isStayLockEngaged = false;
    }

    public void PrioritizeFire(string enemyTag)
    {
        enemyToFocusOn = enemyTag;
        Debug.Log("priortizer received - enemy to focus on:" + enemyToFocusOn);
    }
    #endregion

    #region UPGRADES
    //BRANCH ONE
    public void B1_UI() //Range
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            Debug.Log("upgrading range..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded..." + range);
        }
        else if (isRocket)
        {
            Debug.Log("upgrading range..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded..." + range);
        }
        else if (isCannon)
        {
            Debug.Log("upgrading range..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded..." + range);
        }
    }

    public void B1_U2() //Passive ability OR further upgrade range
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            Debug.Log("upgrading range..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded..." + range);
        }
        else if (isRocket)
        {
            Debug.Log("upgrading range..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded..." + range);
        }
        else if (isCannon)
        {
            Debug.Log("upgrading range p2..." + range);
            range *= 1.20f;
            Debug.Log("range upgraded p2..." + range);
        }
    }

    public void B1_U3() //Spawn ranged version of turret
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            node.SpawnSniperRifle();
        }
        else if (isRocket)
        {
            node.SpawnSwarmRocket();
        }
        else if (isCannon)
        {
            node.SpawnRangeCannon(); //How does it find the node it is placed on?
        }
    }

    //BRANCH TWO
    public void B2_UI()
    {
        if (isLaser)
        {

        }
        else if (isGun) //Fire rate
        {
            Debug.Log("upgrading firerate..." + fireRate);
            fireRate *= 1.20f;
            Debug.Log("firerate upgraded..." + fireRate);
        }
        else if (isRocket) //Burst size
        {

        }
        else if (isCannon) //Fire rate
        {
            Debug.Log("upgrading firerate..." + fireRate);
            fireRate *= 1.20f;
            Debug.Log("firerate upgraded..." + fireRate);
        }
    }

    public void B2_U2()
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            Debug.Log("upgrading firerate..." + fireRate);
            fireRate *= 1.20f;
            Debug.Log("firerate upgraded..." + fireRate);
        }
        else if (isRocket)
        {

        }
        else if (isCannon)
        {
            Debug.Log("upgrading firerate..." + fireRate);
            fireRate *= 1.20f;
            Debug.Log("firerate upgraded..." + fireRate);
        }
    }

    public void B2_U3()
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            node.SpawnGatlingGun();
        }
        else if (isRocket)
        {
            node.SpawnBurstRocket();
        }
        else if (isCannon)
        {
            node.SpawnFireRateCannon();
        }
    }



    //BRANCH THREE
    public void B3_UI()
    {
        if (isLaser)
        {

        }
        else if (isGun) //Power increased
        {
            bullet.GetComponent<Bullet>().damageDefault *= 1.2f;
        }
        else if (isRocket) //Splash range increased. Later I can increase size of center, to make it more powerful
        {
            Debug.Log("upgrading splash damage radius");
            bullet.GetComponent<Bullet>().explosionRadiusCenter *= 1.1f;
            bullet.GetComponent<Bullet>().explosionRadiusMiddle *= 1.1f;
            bullet.GetComponent<Bullet>().explosionRadiusOuter *= 1.1f;
        }
        else if (isCannon) // Mortar, high AoE
        {

        }
    }

    public void B3_U2()
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            bullet.GetComponent<Bullet>().damageDefault *= 1.2f;
        }
        else if (isRocket) //Actual splash damage increaed
        {
            Debug.Log("upgrading splash damage radius");
            bullet.GetComponent<Bullet>().damageDefault *= 1.1f; //Center
            bullet.GetComponent<Bullet>().damageMiddle *= 1.1f;
            bullet.GetComponent<Bullet>().damageOuter *= 1.1f;
        }
        else if (isCannon)
        {

        }
    }

    public void B3_U3()
    {
        if (isLaser)
        {

        }
        else if (isGun)
        {
            node.SpawnPowerRifle();
        }
        else if (isRocket)
        {
            node.SpawnClusterRocket();
        }
        else if (isCannon)
        {
            node.SpawnMortarCannon();
        }
    }

    #endregion

}

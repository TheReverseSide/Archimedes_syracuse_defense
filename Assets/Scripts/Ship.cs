using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

    [Header("SHOOTING STUFF")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;

    private Wall targetWall;
    private Transform target;

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    public string wallTag = "Wall";

    private ShipAI shipAI;
    public Transform closestNode;

    [Header("MISC")]
    public bool ranged;

    public float startSpeed = 10f;
	public float speed;
	public float startHealth = 100;
	private float health;
	public int worth = 50;
	public GameObject deathEffect;

	public Image healthBar;
	private bool isDead = false;

    private void Awake()
    {
        shipAI = GetComponent<ShipAI>();
    }
    
    void Start ()
	{
		speed = startSpeed;
		health = startHealth;
        target = shipAI.closestNode;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void Update() {
        fireCountdown -= Time.deltaTime;
	}

    #region TARGET FINDING AND SHOOTING
    void UpdateTarget(){
        if (target == null)
        {
            Debug.Log("Null target???");
            return;
        }
        else if (ranged)
        {
            //Check if in range
            float distanceToNode = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToNode < range && fireCountdown <= 0f)
            {
                Shoot();
                shipAI.StopNav();
                fireCountdown = 1f / fireRate;
            }
        }
        else
        {
            //Drive to target and crash into wall
            // Debug.Log("Drive to target and crash into wall");
        }
    }

	// void LockOnTarget()
    // {
    //     Vector3 dir = target.position - transform.position;
    //     Quaternion lookRotation = Quaternion.LookRotation(dir);
    //     Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    //     partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    // }

    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

	#endregion SHOOTING SHIT

	#region TAKING DAMAGE, DEATH, EFFECTS
	public void TakeDamage (float amount)
	{
		health -= amount;
        // Debug.Log("Ship hit");

		// healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	public void Slow (float pct)
	{
        speed = startSpeed * pct;
	}

	void Die ()
	{
		isDead = true;

		PlayerStats.Money += worth;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}
	#endregion Damage,death,etc
}
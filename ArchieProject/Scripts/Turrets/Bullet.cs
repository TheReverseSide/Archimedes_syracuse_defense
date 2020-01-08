using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    private Transform target;//we need to access the target already determined by Turret
    public float bulletSpeed = 70f;

    public GameObject impactEffect;

    public float explosionRadiusCenter = 0f;
    public float explosionRadiusMiddle = 0f;
    public float explosionRadiusOuter = 0f;

    public float damageDefault = 0f;
    public float damageMiddle = 0f;
    public float damageOuter = 0f;

    public bool impactElectricEffect;

    bool isSimpleEnemy;
    bool isSwarmEnemy;
    bool isToughEnemy;
    bool isFastEnemy;
    bool isEtherealEnemy;
    bool isBioPhage;

    Transform firePoint;

    bool ParabolaArcActivated;

    protected float AnimationDuration; //Serves as a timer, starting at zero
    float parabolaHeight = 5f;
    #endregion Variables


    public void Seek(Transform _target) //this provides the setup 
    {
        target = _target;
    }

    void Update()
    {
        if (target == null) //we add this in case the target disappears while the bullet is in midair
        {
            Destroy(gameObject);
            return;    //add return because Destroy can take awhile 
        }


        //if we havent hit anything, then keep moving (only if it isnt a mortar)
        if (!ParabolaArcActivated)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = bulletSpeed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame) // if length (magnitude) of dir vector is less than or equal to distance
            { //needed to move this frame, then we hit something
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World); //normalized to make sure that we move at a constant speed (distanceThisFrame)
                                                                                  //Move relative to world space, not local space to avoid weirdness 
                                                                                  //Add rotation (for the missile to look at its target
        }
        else
        {
            Debug.Log(target.transform.position);
            Debug.Log(target.gameObject.name);

            AnimationDuration += Time.deltaTime;

            AnimationDuration = AnimationDuration % 5f; //Counts up to 5, then resets at 0, cycles like that

            transform.position = MathParabola.Parabola(firePoint.position, target.position * 10, parabolaHeight, AnimationDuration / 5f); //Later sub: Vector3.zero with firePoint and Vector3.forward with enemy last known position

        }
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f); //delay before destroying particle effects

        if (explosionRadiusCenter > 0f) Explode();
        else Damage(target, damageDefault);
        Destroy(gameObject);
    }

    void Damage(Transform enemy, float damage)
    {
        Enemy e = enemy.GetComponent<Enemy>(); //called e to differentitate between this variable and the entire object, also called enemy
                                               //this gets enemy script from enemy prefab, attaching it to our e here for use

        foreach (Transform child in e.transform)
        {
            if (gameObject.CompareTag("EnemyFast"))
            {
                isFastEnemy = true;
                AudioManager.instance.Play("SwarmFastEnemyHit"); //audiomanager.instance ?
            }
            else if (gameObject.CompareTag("SwarmEnemy"))
            {
                isSwarmEnemy = true;
                AudioManager.instance.Play("SwarmFastEnemyHit"); //audiomanager.instance ?
            }
            else if (gameObject.CompareTag("EnemySimple"))
            {
                isSimpleEnemy = true;
                AudioManager.instance.Play("SimpleEnemyHit");
            }
            else if (gameObject.CompareTag("EnemyTough"))
            {
                isToughEnemy = true;
                AudioManager.instance.Play("ToughEnemyHit");
            }
            else if (gameObject.CompareTag("EtherealEnemy"))
            {
                isEtherealEnemy = true;
                // AudioManager.instance.Play("");
            }
            else if (gameObject.CompareTag("BioPhageEnemy"))
            {
                isBioPhage = true;
                //AudioManager.instance.Play("");
            }
        }

        if (impactElectricEffect)
        {
            if (isEtherealEnemy || isBioPhage) //Needs to temprarily disale their special abilities when hit with electricty or when they walk within range
            {

            }
        }

        if (e != null) //making sure there is an enemy to damage, in case we forget to label something as enemy
        {
            //Sort for types of damage




            e.TakeDamage(damage);
        }
    }

    void Explode() //creating splash damage - Three layers 
    {
        Collider[] collidersCenter = Physics.OverlapSphere(transform.position, explosionRadiusCenter);  //shoots out a sphere (explosion radius) and returns (an array) all of the colliders that were hit by the sphere
        Collider[] collidersMiddle = Physics.OverlapSphere(transform.position, explosionRadiusMiddle);
        Collider[] collidersOuter = Physics.OverlapSphere(transform.position, explosionRadiusOuter);

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyFast"))
            {
                AudioManager.instance.Play("SwarmFastEnemyHit");
            }
            else if (child.CompareTag("SwarmEnemy"))
            {
                AudioManager.instance.Play("SwarmFastEnemyHit");
            }
            else if (child.CompareTag("EnemySimple"))
            {
                AudioManager.instance.Play("SimpleEnemyHit");
            }
            else if (child.CompareTag("EnemyTough"))
            {
                AudioManager.instance.Play("ToughEnemyHit");
            }
        }

        foreach (Transform child in transform)
        {
            if (gameObject.CompareTag("Rocket1_2"))
            {
                AudioManager.instance.Play("RocketImpact1_2");
            }
            else if (child.CompareTag("Rocket3"))
            {
                AudioManager.instance.Play("RocketImpact3");
            }
            else if (child.CompareTag("MortarShell1_2")) //CUIDADO: Maybe mortar doesnt use explode after all. It has its own method.
            {
                AudioManager.instance.Play("MortarExplosion1_2");
            }
            else if (child.CompareTag("MortarShell3"))
            {
                AudioManager.instance.Play("MortarExplosion3");
            }
        }

        //filter for enemy tags
        foreach (Collider col in collidersOuter)
        {
            if (col.CompareTag("Enemy"))
            {
                Damage(col.transform, damageOuter);
            }
        }
        foreach (Collider col in collidersMiddle)
        {
            if (col.CompareTag("Enemy"))
            {
                Damage(col.transform, damageMiddle - damageOuter);
            }
        }
        foreach (Collider col in collidersCenter)
        {
            if (col.CompareTag("Enemy"))
            {
                Damage(col.transform, (damageDefault - damageOuter - damageMiddle));
            }
        }
    }

    public void ParabolaArc(Transform firePoint, Transform target)
    { //This may not work because I dont have an update method to continually update it
        ParabolaArcActivated = true;

        this.firePoint = firePoint;
        this.target = target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadiusOuter);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadiusMiddle);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadiusCenter);
    }
}

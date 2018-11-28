using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] GameObject hit_audio_prefab;

    //EnemyMovement
    //private Animator animator;
    public float speed;
    public float rotatingSpeed;
    private Transform target;

    //EnemyAttack
    [SerializeField]
    GameObject bullet;
    public float firingRate;
    public float nextShot;
    public float timer;

    //Explosion
    [SerializeField]
    //GameObject ExplosionPrefab;

    PlayerControl playerContScript;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public int enemyHP = 4;

    //Getter and setter to get players position
    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //Added
        GameObject thePlayer = GameObject.Find("Aris");
        playerContScript = thePlayer.GetComponent<PlayerControl>();
        firingRate = 1f;
        nextShot = Time.time;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (target != null && (playerContScript.current_hp != 0 || playerContScript.current_hp < 0))
        {
            //If there is a target it will rotate towards it + 90deg makes it face the player 
            float rotationDeg = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg + 90f;
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.MoveTowardsAngle(transform.eulerAngles.z, rotationDeg, Time.deltaTime * rotatingSpeed));
            shoot();
        }
        else if (target == null)
        {

        }
    }

    public int getEnemyHP()
    {
        return enemyHP;
    }
    public void reduceEnemyHP()
    {
        reduceEnemyHP(1);
    }

    public void reduceEnemyHP(int value)
    {
        enemyHP -= value;
        if (hit_audio_prefab != null) Instantiate(hit_audio_prefab);

        if (enemyHP <= 0)
            Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void shoot()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + firingRate;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)//alice tools
        {
            AliceWeapon aw = other.gameObject.GetComponent<AliceWeapon>();
            reduceEnemyHP(aw.damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //BossMovement
    public Animator animator;
    public float speed;
    private Transform target;
    protected Vector2 direction;

    //BossAttack
    [SerializeField]
    GameObject bullet;
    public float firingRate;
    public float nextShot;
    public float timer;

    //RandomWalk Script
    BossRandomWalk bossRandomWalk;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    int enemyHP = 5;
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
        animator = GetComponent<Animator>();
        firingRate = 1f;
        nextShot = Time.time;
        bossRandomWalk = GetComponent<BossRandomWalk>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // animatedDirection(direction);
        timer += Time.deltaTime;
        if (target != null)
        {
            RunFromTarget();
        }
        else if (target == null)
        {
            bossRandomWalk.FollowRandomDirection();
        }
    }

    //When the player is collided with the enemy it will run the opposite way
    public void RunFromTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            rb2d.velocity = -((direction) * 200 * Time.deltaTime);

            if (timer >= 3f)
            {
                attacking();
                timer = 0;
            }
        }
    }

    //Attack at a firing rate
    private void attacking()
    {
        if (Time.time > nextShot)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextShot = Time.time + firingRate;
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
        if (enemyHP <= 0)
            Die();

        Debug.Log(enemyHP);
    }
    public void Die()
    {
        Destroy(gameObject);
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

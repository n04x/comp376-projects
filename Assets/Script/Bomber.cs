using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour {

    [SerializeField] GameObject hit_audio_prefab;
    [SerializeField] GameObject explosion_audio_prefab;


    //EnemyMovement
    //private Animator animator;
    public float speed;
    private Transform target;
    protected Vector2 direction;

    //EnemyAttack
    [SerializeField]
    GameObject bullet;
    public float firingRate;
    public float nextShot;
    public float timer;

    //RandomWalk Script
    EnemyRandomWalk EnemyRngWalk;

    //Explosion
    [SerializeField]
    GameObject ExplosionPrefab;

    private PlayerControl playerContScript;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
      [SerializeField] int enemyHP;

    bool triggered = false;
    float explosionTimer = 1f;
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
    void Start () {
        //Added
        GameObject thePlayer = GameObject.Find("Aris");
        playerContScript = thePlayer.GetComponent<PlayerControl>();
        //animator = GetComponent<Animator>();
        firingRate = 1f;
        nextShot = Time.time;
        EnemyRngWalk = GetComponent<EnemyRandomWalk>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyHP = 4 + NextLevel.currentLevel;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //animatedDirection(direction);
        timer += Time.deltaTime;

        if (target != null)
        {
            //direction = Vector2.zero;
            RunToTarget();
        }
        else if (target == null)
        {
           EnemyRngWalk.FollowRandomDirection();
        }

	}

    //Set animation for all 4 direction using animator and variable
    private void animatedDirection(Vector2 direction)
    {
        //animator.SetFloat("x", direction.x);
        //animator.SetFloat("y", direction.y);
    }

    //When the player is collided with the enemy it will run the opposite way
    private void RunToTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            rb2d.velocity = (direction)*speed*Time.deltaTime;
        }
    }

    public int getEnemyHP()
    {
        return enemyHP;
    }
    public void reduceEnemyHP()
    {
        
        reduceEnemyHP (1);
    }

    public void reduceEnemyHP(int value){
        enemyHP -= value;
	   if(hit_audio_prefab!=null) Instantiate(hit_audio_prefab);

        if(enemyHP <=0)
        Die();
    }
    public void Die(){
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        if(explosion_audio_prefab!=null) Instantiate(explosion_audio_prefab);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other){
      

        if(other.gameObject.tag == "Player")
        {
         triggered = true;
        } 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
          if(other.gameObject.layer ==9)//alice tools
        {
            //Debug.Log("Hit");
            if(other.gameObject.tag == "AliceSword")
                Die();

            AliceWeapon aw = other.gameObject.GetComponent<AliceWeapon>();
            reduceEnemyHP(aw.damage);
        }
    }

    void OnCollisionStay2D(Collision2D other){
                explosionCheck();

    }
    void explosionCheck(){
        if(triggered)
        {
            explosionTimer -= Time.deltaTime * (1.0f + (NextLevel.currentLevel * 0.05f));
            if(explosionTimer <= 0)
            {
            playerContScript.takeDamage(transform.position,30);      
            Die();
            }
        }
    }
}

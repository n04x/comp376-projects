using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject hit_audio_prefab;

    //EnemyMovement
    private Animator animator;
    public float speed;
    private Transform target;
    protected Vector2 direction;

    //EnemyAttack
    [SerializeField]
    GameObject bullet, beamPrefab;
    float timer;
    public float firingRate;
    public float shotDelay;
    public int numbShots;

    bool shootBeam = false;

    //RandomWalk Script
    EnemyRandomWalk EnemyRngWalk;

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
    void Start () {
        animator = GetComponent<Animator>();
        EnemyRngWalk = GetComponent<EnemyRandomWalk>();
        timer = 0.0f;
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        animatedDirection(direction);
        timer += Time.deltaTime;

        if (target != null)
        {
            RunFromTarget();

            if (shootBeam == true)
                beam();


            else
                shoot();

            //beam();
            //beamAOE();
        }
        else if (target == null)
        {
           EnemyRngWalk.FollowRandomDirection();
        }
	}

    //Set animation for all 4 direction using animator and variable
    private void animatedDirection(Vector2 direction)
    {
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    //When the player is collided with the enemy it will run the opposite way
    public void RunFromTarget()
    {

        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            rb2d.velocity = -((direction) * 200 * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, target.position,-1* speed * Time.deltaTime);

        }
    }

    //Base Attack
    private void shoot()
    {
        
        if (numbShots == 0)
        {
            Debug.Log("DELAY: " + timer);

            if (timer > shotDelay)
            {
                numbShots = 3;
                shootBeam = true;
            }
                
        }
        

        else if (timer > firingRate)
        {
            //Debug.Log("SHOOT: " + timer);
            Instantiate(bullet, transform.position, Quaternion.identity);
            numbShots--;
            timer = 0;
        }

    }

    //Beam attack
    private void beam()
    {

        if (timer > 3.0f)
        {
            Instantiate(beamPrefab, transform.position, Quaternion.identity);

            if (timer > 6)
            {
                timer = 0;
                shootBeam = false;
            }
                
        }

    }

    //Beam attack
    private void beamAOE()
    {

        if (timer > 3.0f)
        {
            Instantiate(beamPrefab, transform.position, Quaternion.identity);

            if (timer > 6)
                timer = 0;
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
        ScoreController.Increment(ScoreController.KNIGHT_SCORE);

        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer ==9)//alice tools
        {
            AliceWeapon aw = other.gameObject.GetComponent<AliceWeapon>();
            reduceEnemyHP(aw.damage);
        }
    }
}

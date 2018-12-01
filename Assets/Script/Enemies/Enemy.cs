using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject hit_audio_prefab;
    [SerializeField] AudioSource aoeSound;
    bool playBeamSound = true;

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
    public float rotatingSpeed;

    bool shootBeam = false;

    //RandomWalk Script
    EnemyRandomWalk EnemyRngWalk;
    AudioSource shootSound;

    private Rigidbody2D rb2d;      
    int enemyHP;
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
        enemyHP = 4 + NextLevel.currentLevel;
        shotDelay -= NextLevel.currentLevel * 0.05f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        shootSound = GetComponent<AudioSource>();

        if (target != null)
        {
            RunFromTarget();
            shoot();
        }
        else if (target == null)
        {
           EnemyRngWalk.FollowRandomDirection();
        }
	}

    //When the player is collided with the enemy it will run the opposite way
    public void RunFromTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            rb2d.velocity = -((direction) * 200 * Time.deltaTime);
            //If there is a target it will rotate towards it + 90deg makes it face the player 
            float rotationDeg = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg + 90f;
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.MoveTowardsAngle(transform.eulerAngles.z, rotationDeg, Time.deltaTime * rotatingSpeed));
            shoot();
        }
    }

    //Base Attack
    private void shoot()
    {     
        if (numbShots == 0)
        {
            if (timer > shotDelay)
            {
                numbShots = 3;
                shootBeam = true;
            }          
        }
        else if (timer > firingRate)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            shootSound.Play();
            numbShots--;
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

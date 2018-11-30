using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float timer;
    public float moveTimer;
    public float moveTimerLeft;
    float bossCurrentHP;
    public NextLevel nextLevel;
    float bossMaxHP = 25 + NextLevel.currentLevel;

    public GameObject shootSound, aoeSound;
    bool playBeamSound = true;

    // Visuals
    public ParticleSystem normalBeat;
    public ParticleSystem criticalBeat;

    //Boss Movement
    private Rigidbody2D rb;
    public float speed;

    //Direction
    float randomX;
    float randomY;
    Vector2 direction;
    Vector2 direction1;

    //Sprite shift 
    bool facingRight = false;

    //Random Movement
    public float accelerationTime = 2f;
    private Vector2 movement;
    private float timeLeft;

    //Moving phase + shooting
    public int numbShots;
    public float shotDelay;
    public float firingRate;

    //Attacks
    bool shootBeam = false;
    [SerializeField]
    public GameObject bullet, beamPrefab, beamAOEPrefab, beamFollow, nextLevelPrefab;
    Quaternion rotation;
    float bulletSpeed = 10f;
    bool actionFinish = false;

    //Getter and Setter to get players position
    private Transform target;
    private PlayerControl playerContScript;
    private Vector2 playerDirection;
    public float speedToPlayer;
    bool movingBool = false;
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

    //Start is called before the first frame update
    void Start()
    {
        beamPrefab.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody2D>();
        GameObject thePlayer = GameObject.Find("Aris");
        playerContScript = thePlayer.GetComponent<PlayerControl>();
        rotation = Quaternion.Euler(0, 180, 0);
        direction = new Vector2(0f, 1f) * bulletSpeed;
        direction1 = new Vector2(1f, 0f) * bulletSpeed;
        bossCurrentHP = bossMaxHP;
    }

    //Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        moveTimer += Time.deltaTime;
      
        if (movingBool == true)
        {
            movingUpdate();
        }

        //Different boss phase
        switch (state)
        {
            case State.IDLE:
                idleUpdate();
                break;
            case State.ATTACK_1:
                attack1_Update();
                break;
            case State.ATTACK_2:
                attack2_Update();
                break;
            case State.ATTACK_3:
                attack3_Update();
                break;
            case State.ATTACK_4:
                attack4_Update();
                break;
        }
        
        if(actionFinish == true)
        {
          RandomizeState();
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
        rb.velocity = (movement * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        //Flip the enemy left and right
        Vector3 localScale = transform.localScale;
        if (movement.x > 0)
        {
            facingRight = true;
        }
        else if (movement.x < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    public enum State
    {
        IDLE,
        ATTACK_1,
        ATTACK_2,
        ATTACK_3,
        ATTACK_4
    }

    //Serialize so can access in inspector
    [SerializeField]
    public State state = State.IDLE;

    //Phase 2 occur when half hp
    void RandomizeState()
    {
        if(bossCurrentHP <= (bossMaxHP / 2))
        {
            state = (State)Random.Range(1, 5);
            actionFinish = false;
        }
        else
        {
            state = (State)Random.Range(3, 5);
            actionFinish = false;
        }
    }
    

    //Different state of the boss
    void idleUpdate()
    {
        //Stop few seconds and aoe for few seconds
        movement = Vector2.zero;
        movingBool = false;
        actionFinish = true;
    }

    void movingUpdate()
    {
        //Move boss and shoot at player at the same time
        movingBool = true;
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);
        moveTimerLeft -= Time.deltaTime;

        if (moveTimerLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            moveTimerLeft += accelerationTime;
            shoot();
            actionFinish = true;
        }
    }

    //beam attack
    void attack1_Update()
    {
        //Laser beam multiple direction
        movingBool = true;

        if (playBeamSound == true)
        {
            if (aoeSound != null) Instantiate(aoeSound);
            playBeamSound = false;
        }

        if (timer > 3.0f)
        {
            GameObject beamPrefab0 = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beamPrefab0.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);

            GameObject beamPrefab1 = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beamPrefab1.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, -direction.y);

            GameObject beamPrefab2 = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beamPrefab2.GetComponent<Rigidbody2D>().velocity = new Vector2(direction1.x, direction1.y);

            GameObject beamPrefab3 = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beamPrefab3.GetComponent<Rigidbody2D>().velocity = new Vector2(-direction1.x, direction1.y);

            if (timer > 6)
            {
                timer = 0;
                shootBeam = false;
                actionFinish = true;
                playBeamSound = true;
            }
        }
    }

    void attack2_Update()
    {
        //AoE while moving
        movingBool = true;

        if (timer > 3.0f)
        {
            Instantiate(beamAOEPrefab, transform.position, Quaternion.identity);
            if (playBeamSound == true)
            {
                if (aoeSound != null) Instantiate(aoeSound);
                playBeamSound = false;
            }
            if (timer > 6)
            {
                timer = 0;
                shootBeam = false;
                actionFinish = true;
                playBeamSound = true;
            }
        }
    }

    //Shoot beam following the player
    void attack3_Update()
    {
        movingBool = true;

        if (timer > 3.0f)
        {
            Instantiate(beamFollow, transform.position, Quaternion.identity);
            if (playBeamSound == true)
            {
                if (aoeSound != null) Instantiate(aoeSound);
                playBeamSound = false;
            }

            if (timer > 6)
            {
                timer = 0;
                shootBeam = false;
                actionFinish = true;
                playBeamSound = true;
            }
        }
    }

    //Aggro Attack - Randomly go to player + triple burst
    void attack4_Update()
    {
        if (timer < 6)
        {
            if (target != null)
            {
                movement = (target.position - transform.position).normalized;
            }
            else
            {
                movement = Vector2.zero;
            }
                rb.velocity = (movement) * speedToPlayer * Time.deltaTime;
                shoot();
                actionFinish = true;  
        }
    }

    void die()
    {
        // Stop heartbeat
        criticalBeat.Stop();  
        movement = Vector2.zero;
    }

    //Moving phase shooting bullets
    private void shoot()
    {
        playBeamSound = true;

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
            if (shootSound != null) Instantiate(shootSound);
            numbShots--;
            timer = 0;
        }
    }

    //Reduce boss Hp
    public void reduceBossHP()
    {
        reduceBossHP(1);
    }

    //Reduce boss Hp with a value
    public void reduceBossHP(int value)
    {
        bossCurrentHP -= value;
        if (bossCurrentHP <= 0)
        {
            GameObject nextLevelPortal = Instantiate(nextLevelPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);   
            ScoreController.Increment(ScoreController.BOSS_SCORE);
            Destroy(gameObject);
        }
        else if (bossCurrentHP <= bossMaxHP/2) 
        {
            normalBeat.Stop();
            criticalBeat.Play();
        }
    }


    //Enemy will reassign different direction
    //when collide with wall in the room
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("room"))
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        //alice tools
        if (other.gameObject.layer == 9)
        {
            AliceWeapon aw = other.gameObject.GetComponent<AliceWeapon>();
            reduceBossHP(aw.damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("room"))
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}

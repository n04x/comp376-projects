﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float timer;
    float bossHP = 25;

    //Boss Movement
    private Rigidbody2D rb;
    public float speed;

    //Direction
    float randomX;
    float randomY;
    Vector2 direction;
    //Vector2 movement;

    //Animation 
    public Animator animator;
    bool facingRight = false;

    //Random Movement
    public float accelerationTime = 2f;
    //public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    //Moving phase + shooting
    public int numbShots;
    public float shotDelay;
    public float firingRate;

    //Attacks
    bool shootBeam = false;
    [SerializeField]
    GameObject bullet, beamPrefab;

    //Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        switch (state)
        {
            case State.IDLE:
                idleUpdate();
                break;
            case State.MOVING:
                movingUpdate();
                break;
            case State.ATTACK_1:
                attack1_Update();
                break;
            case State.ATTACK_2:
                attack2_Update();
                break;
            case State.DIE:
                die();
                break;
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
        MOVING,
        ATTACK_1,
        ATTACK_2,
        DIE
    }
    //Serialize so can access in inspector
    [SerializeField]
    public State state = State.IDLE;

    //Different state of the boss
    //
    void idleUpdate()
    {
        //Stop few seconds and aoe for few seconds
        movement = Vector2.zero;
        /*if (timer > 3.0f)
        {
            Instantiate(beamPrefab, transform.position, Quaternion.identity);
            if (timer > 6)
                timer = 0;
        }
        */
        animator.SetBool("isRunning", false);
    }

    void movingUpdate()
    {
        //Move boss and shoot at player at the same time
        //Moving at random place in the room
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
            shoot();
        }
        
        animator.SetBool("isRunning", true);
    }

    //beam attack
    void attack1_Update()
    {
        movement = Vector2.zero;
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

    void attack2_Update()
    {
        movement = Vector2.zero;
        //stay and laser beam multiple direction
    }

    void die()
    {
        movement = Vector2.zero;
        //die when reach 0
        if(bossHP <= 0)
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject);
        }
    }

    //Moving phase shooting
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("room"))
        {
            Debug.Log("bump");
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("room"))
        {
            Debug.Log("bump");
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
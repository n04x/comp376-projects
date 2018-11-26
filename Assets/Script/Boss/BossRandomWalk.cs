using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomWalk : MonoBehaviour
{
    public Transform areaTarget;
    public float bossSpeed;
    public float timer;
    public float minCoordX;
    public float minCoordY;
    public float maxCoordX;
    public float maxCoordY;
    protected Vector2 direction;
    public Animator animator;
    Boss boss;
    bool facingRight = false;



    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        init();
    }

    public void init()
    {
        boss = GetComponent<Boss>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animatedDirection(direction);
        timer += Time.deltaTime;
    }

    //To flip the animation for the boss
    void LateUpdate()
    {
        Vector3 localScale = transform.localScale;
        if(direction.x > 0)
        {
            facingRight = true;
        }
        else if (direction.x < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    private void animatedDirection(Vector2 direction)
    {
        direction = (areaTarget.transform.position - transform.position).normalized;
        animator.SetFloat("x", Mathf.Abs(direction.x)); 
        animator.SetFloat("y", Mathf.Abs(direction.y));
       // animator.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void FollowRandomDirection()
    {
        direction = (areaTarget.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, areaTarget.position, bossSpeed * Time.deltaTime);
        rb.velocity = direction * bossSpeed;
        if (Vector2.Distance(transform.position, areaTarget.position) < 1f)
        {
            if (timer <= 4)
            {
                animatedDirection(direction);
                areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
            }
            else
            {
                timer = 0;
            }
        }
    }

    //Added this if enemy bumps enemy it will assign a new direction randomly
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag == "Enemy")
        {
            Debug.Log("Bump");
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }

        if (other.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            Debug.Log("Bump");
            boss.RunFromTarget();
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            Debug.Log("Bump");
            boss.RunFromTarget();
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomWalk : MonoBehaviour {

    public Transform areaTarget;
    public float enemySpeed;
    public float timer;
    public float minCoordX;
    public float minCoordY;
    public float maxCoordX;
    public float maxCoordY;
    protected Vector2 direction;
    private Animator animator;
    Enemy enemy;
   
    Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        init();
	}

    public void init()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY,maxCoordY));
    }

	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
    }

    public void FollowRandomDirection()
    {
            direction = (areaTarget.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, areaTarget.position, enemySpeed * Time.deltaTime);
            if(enemy != null&&enemy.Target == null){
            float rotationDeg = Mathf.Atan2(transform.position.y - areaTarget.position.y, transform.position.x - areaTarget.position.x) * Mathf.Rad2Deg + 90f;
           transform.eulerAngles = new Vector3(0f, 0f, Mathf.MoveTowardsAngle(transform.eulerAngles.z, rotationDeg, Time.deltaTime * 400f));
            }
            rb.velocity = direction * enemySpeed;
            if (Vector2.Distance(transform.position, areaTarget.position) < 1f)
            {
                if (timer <= 4)
                {
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
        if(other.gameObject.tag == "Enemy" && gameObject.tag == "Enemy")
        {
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }

        if (other.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            enemy.RunFromTarget();
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            enemy.RunFromTarget();
            areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY, maxCoordY));
        }
    }
}

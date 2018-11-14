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
        animator = GetComponent<Animator>();
         rb = GetComponent<Rigidbody2D>();
        areaTarget.position = new Vector2(Random.Range(minCoordX, maxCoordX), Random.Range(minCoordY,maxCoordY));
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        animatedDirection(direction);
        timer += Time.deltaTime;
    }
    
    private void animatedDirection(Vector2 direction)
    {
        direction = (areaTarget.transform.position - transform.position).normalized;
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    public void FollowRandomDirection()
    {
            direction = (areaTarget.transform.position - transform.position).normalized;
          //  transform.position = Vector2.MoveTowards(transform.position, areaTarget.position, enemySpeed * Time.deltaTime);
            rb.velocity = direction * enemySpeed;
            if (Vector2.Distance(transform.position, areaTarget.position) < 0.2f)
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
}

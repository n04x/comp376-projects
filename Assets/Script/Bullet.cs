using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float bulletSpeed = 5f;
    Rigidbody2D rb;

    PlayerControl target;
    Vector2 direction;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerControl>();
        direction = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector2(direction.x, direction.y);
        Destroy(gameObject, 3f);
	}
	
    //Destroy bullet 
	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float bulletSpeed = 10.0f;
    private Rigidbody2D rb2d;

    PlayerControl target;
    Vector2 direction;

    private PlayerControl playerContScript;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerControl>();

        GameObject thePlayer = GameObject.Find("Aris");
        playerContScript = thePlayer.GetComponent<PlayerControl>();

        direction = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.name.Equals("Aris"))
        {
            //Cannot manually set the damage taken

            playerContScript.takeDamage(transform.position, 10);


        }
                    Destroy(gameObject);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAOE : MonoBehaviour
{

    float bulletSpeed = 5f;
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

        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * bulletSpeed;

        rb2d.velocity = new Vector2(direction.x, direction.y);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.name.Equals("Aris"))
        {
            //Cannot manually set the damage taken
            //Debug.Log(playerContScript.current_hp); 
            playerContScript.takeDamage(transform.position,10);
        }
                    Destroy(gameObject);
    }
}

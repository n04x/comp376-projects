using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    public float bulletSpeed = 5f;
    private Rigidbody2D rb2d;

    Vector2 direction;
    private PlayerControl playerContScript;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GameObject thePlayer = GameObject.Find("Aris");
        playerContScript = thePlayer.GetComponent<PlayerControl>();
    }

    //Damage alice 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Aris"))
        {
            playerContScript.takeDamage(transform.position,20);
        }
        Destroy(gameObject);
    }
}

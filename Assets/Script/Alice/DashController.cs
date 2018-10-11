using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerControl alice;
    private Rigidbody2D rb;
    public float dashValue = 5f;
    [SerializeField]
    float DASH_DURATION = .1f;
    [SerializeField]
     float INVINCIBILITY_DURATION = .1f;
    float dash_timer;
    float invincibility_timer;
    void Start()
    {
        alice = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();

        dash_timer = DASH_DURATION;
        invincibility_timer = INVINCIBILITY_DURATION;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void FixedUpdate()
    {

        //Dash
        if (Input.GetButtonDown("LB") && Mathf.Abs(rb.velocity.magnitude) >= 0.1f)
        {
            alice.isDashing = true;
            alice.isInvincible = true;
            rb.AddForce(alice.lStickDir.normalized * dashValue * alice.movementSpeed * Time.deltaTime, ForceMode2D.Impulse);

        }
        if(alice.isDashing)
        {
            dash_timer -= Time.deltaTime;
        }
        if(alice.isDashing && dash_timer <= 0f)
        {
            dash_timer = DASH_DURATION;
            alice.isDashing = false;
        }

        if (alice.isInvincible)
        {
            invincibility_timer -= Time.deltaTime;

            //PLACEHOLDER TO INDICATE INV
            GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
        }
        if (alice.isInvincible && invincibility_timer <= 0f)
        {
            invincibility_timer = INVINCIBILITY_DURATION;
            alice.isInvincible = false;

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        }
    }
}

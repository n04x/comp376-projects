using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : BlackJackAffected
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

    float DASH_COOLDOWN = 1f;
    private float cooldown_timer;
    [SerializeField] int DASH_COUNT = 1;
    [SerializeField] int REMAINING_DASHES = 1;
    void Start()
    {
        alice = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();

        dash_timer = DASH_DURATION;
        invincibility_timer = INVINCIBILITY_DURATION;

        REMAINING_DASHES = DASH_COUNT;
        cooldown_timer = DASH_COOLDOWN;

        suit.diamond = true;
    }


    void Update()
    {
        updateCooldown();
        updateCurrentMode();
        updateTargetMode();
    }
    void FixedUpdate()
    {
        Dash();
    }
    void updateCooldown()
    {
        cooldown_timer += Time.deltaTime;
        if (cooldown_timer >= DASH_COOLDOWN && REMAINING_DASHES < DASH_COUNT)
        {
            REMAINING_DASHES++;
            cooldown_timer = 0;
        }
        DASH_COUNT = 1 + current_mode/7;


    }
    void Dash()
    {
        //Dash
        if (!alice.isSlashing && Input.GetButtonDown("LB") && Mathf.Abs(rb.velocity.magnitude) >= 0.1f
            && REMAINING_DASHES > 0)
        {
            cooldown_timer = 0;
            REMAINING_DASHES--;
            alice.isDashing = true;
            alice.isInvincible = true;
            rb.AddForce(alice.lStickDir.normalized * dashValue * alice.movementSpeed * Time.deltaTime, ForceMode2D.Impulse);

        }

        if (alice.isDashing)
        {
            dash_timer -= Time.deltaTime;
        }
        if (alice.isDashing && dash_timer <= 0f)
        {
            dash_timer = DASH_DURATION;
            alice.isDashing = false;
        }

        if (alice.isInvincible)
        {
            invincibility_timer -= Time.deltaTime;

            //PLACEHOLDER TO INDICATE INV
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        if (alice.isInvincible && invincibility_timer <= 0f)
        {
            invincibility_timer = INVINCIBILITY_DURATION;
            alice.isInvincible = false;

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        }
    }
}

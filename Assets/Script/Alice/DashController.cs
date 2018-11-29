using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : BlackJackAffected
{
    // Start is called before the first frame update

    [SerializeField] GameObject dash_audio_prefab;
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
    public int DASH_COUNT = 1;
    [SerializeField] int REMAINING_DASHES = 1;
    public SpriteRenderer shoulderSprite;
    public Sprite normalShoulders;
    public Sprite invShoulders;

    void Start()
    {
        alice = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();

        dash_timer = DASH_DURATION;
        invincibility_timer = INVINCIBILITY_DURATION;

        REMAINING_DASHES = DASH_COUNT;
        cooldown_timer = 0;

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
        UpdateDashInput();
    }

    void updateCooldown()
    {
        if(REMAINING_DASHES < DASH_COUNT)
        {
            cooldown_timer += Time.deltaTime;
        }
        if (cooldown_timer >= DASH_COOLDOWN && REMAINING_DASHES < DASH_COUNT)
        {
            REMAINING_DASHES++;
            DASH_UI.Refresh(REMAINING_DASHES);

            cooldown_timer = 0;
        }
        DASH_COUNT = 1 + current_mode / 7;
        if(REMAINING_DASHES > DASH_COUNT)
        {
            REMAINING_DASHES = DASH_COUNT;
            DASH_UI.Refresh(REMAINING_DASHES);

        }

    }

    void UpdateDashInput()
    {
        if (!alice.isDashing&&!alice.isSlashing && Input.GetButtonDown("LB") && Mathf.Abs(rb.velocity.magnitude) >= 0.1f
                    && REMAINING_DASHES > 0)
        {
            alice.isDashing = true;
            if(dash_audio_prefab!=null) Instantiate(dash_audio_prefab);

            REMAINING_DASHES--;
            
            DASH_UI.Refresh(REMAINING_DASHES);

        }
    }
    void Dash()
    {
        //Dash
        if (alice.isDashing)
        {
            alice.isInvincible = true;
            cooldown_timer = 0;

            rb.AddForce(alice.lStickDir.normalized * dashValue * alice.movementSpeed * Time.deltaTime, ForceMode2D.Impulse);




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

            // Display invincible shoulder sprite
            shoulderSprite.sprite = invShoulders;
        }
        if (alice.isInvincible && invincibility_timer <= 0f)
        {
            invincibility_timer = INVINCIBILITY_DURATION;
            alice.isInvincible = false;

            // Display vulnerable shoulder sprite
            shoulderSprite.sprite = normalShoulders;

        }
    }
}

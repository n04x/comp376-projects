using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    [SerializeField] GameObject Shield;
    [SerializeField] float shield_offset = 0.6f;
    public  int MAX_HP = 20;

    //time it takes for 1 tick to go away.
    public  float SHIELD_DECAY_TICK = .1f;
    public float SHIELD_REGEN_TICK = .05f;
    private PlayerControl alice;
    int current_hp;
    float decay_timer,regen_timer;
    GameObject sh;


    void Start()
    {
        current_hp = MAX_HP;
        decay_timer = SHIELD_DECAY_TICK;
        regen_timer = SHIELD_REGEN_TICK;
        alice = GetComponent<PlayerControl>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!Input.GetButton("RB"))
        {
            alice.shieldOut = false;
            if (sh != null)
            {
                Hide();
            }
        }
        if (Input.GetButtonDown("RB")&& !alice.shieldOut)
        {
            alice.shieldOut = true;
              sh = Instantiate(Shield,transform.position + (transform.up * shield_offset), transform.rotation);
            sh.transform.parent = this.transform;
        }

        if(alice.shieldOut)
        {
            Decay();
        }else
        {
            Regen();
        }
  
    }
    private void Decay()
    {
        decay_timer -= Time.deltaTime;
        if(decay_timer <= 0 )
        {
            if(current_hp > 0)
                current_hp--;
            decay_timer = SHIELD_DECAY_TICK;
        }
        if (sh != null) { 
          SpriteRenderer sr =  sh.GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r,sr.color.g,sr.color.b, (float)current_hp/MAX_HP);

        }
    }

    private void Regen()
    {
        regen_timer -= Time.deltaTime;
        if(regen_timer<=0)
        {
            if (current_hp < MAX_HP)
                current_hp++;
            regen_timer = SHIELD_REGEN_TICK;
        }
    }
    private void Hide()
    {
        Destroy(sh);
    }
}

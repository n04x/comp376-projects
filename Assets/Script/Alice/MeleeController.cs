using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : BlackJackAffected
{
    [SerializeField] GameObject sword;
    [SerializeField] float slash_forward_value = 1;
    public float SLASH_DURATION = 0.1f;
    private float slash_timer;
    Rigidbody2D sword_rb;



    private PlayerControl alice;


    public GameObject sword_instance;
    private Rigidbody2D wielder_rb;

    private float sword_rotation_cap = 90;
    private float sword_rotation_min = 20;
    private float current_rotation;
    // Start is called before the first frame update
    void Start()
    {

        alice = GetComponent<PlayerControl>();
        current_rotation = sword_rotation_min;
        slash_timer = SLASH_DURATION;
        wielder_rb = GetComponent<Rigidbody2D>();
        sword_instance = null;
        suit.club = true;
    }
    void Update(){
        updateCurrentMode();
        updateTargetMode();
        updateCurrentRotation();
        UpdateSlashInput();
    }
    void updateCurrentRotation(){
        current_rotation = sword_rotation_min + (sword_rotation_cap - sword_rotation_min) * ((current_mode)/21f);
    }
    void FixedUpdate()
    {
        


        if(sword_instance != null)
        {
            Slash();
        }
            


    }
 
    public void UpdateSlashInput(){
            if (sword_instance == null && Input.GetButtonDown("RB") && !alice.isSlashing)
        {
            Debug.Log(wielder_rb);
            alice.isSlashing = true;
            wielder_rb.velocity = Vector2.zero;
            sword_instance = Instantiate(sword, transform.position, transform.rotation);
            AliceWeapon alice_sword = sword_instance.gameObject.GetComponentInChildren<AliceWeapon>();
            if(current_mode>0)
            alice_sword.damage = current_mode;
            //pushes alice forward
            wielder_rb.velocity = (transform.up* slash_forward_value);
           // Debug.Log(current_mode +"\n" + current_rotation);
        }
    }
    public void Slash()
    {
        sword_instance.transform.position = this.transform.position;
        Rigidbody2D sw_rb = sword_instance.GetComponent<Rigidbody2D>();
        if (slash_timer > 0)
        {
            slash_timer -= Time.deltaTime;
            
            sw_rb.rotation = Mathf.Lerp(wielder_rb.rotation-current_rotation,wielder_rb.rotation+ current_rotation, slash_timer / SLASH_DURATION) ;
        }
        else
        {
            Destroy(sword_instance);
            sword_instance = null;
            slash_timer = SLASH_DURATION;
            alice.isSlashing = false;
        }
    }


}

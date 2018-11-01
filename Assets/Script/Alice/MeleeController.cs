using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] float slash_forward_value = 1;
    public float SLASH_DURATION = 0.1f;
    private float slash_timer;
    Rigidbody2D sword_rb;



    private PlayerControl alice;


    public GameObject sword_instance;
    private Rigidbody2D wielder_rb;

    private float sword_rotation = 90;
    // Start is called before the first frame update
    void Start()
    {

        alice = GetComponent<PlayerControl>();

        slash_timer = SLASH_DURATION;
        wielder_rb = GetComponent<Rigidbody2D>();
        sword_instance = null;
    }

  
    void FixedUpdate()
    {
            if (sword_instance == null && Input.GetButtonDown("X") && !alice.isSlashing)
        {
            alice.isSlashing = true;
            wielder_rb.velocity = Vector2.zero;
            sword_instance = Instantiate(sword, transform.position, transform.rotation);

            //pushes alice forward

            wielder_rb.velocity = (transform.up* slash_forward_value);
        }


        if(sword_instance != null)
        {
            Slash();
        }
            


    }
 
    public void Slash()
    {
        sword_instance.transform.position = this.transform.position;
        Rigidbody2D sw_rb = sword_instance.GetComponent<Rigidbody2D>();
        if (slash_timer > 0)
        {
            slash_timer -= Time.deltaTime;
            
            sw_rb.rotation = Mathf.Lerp(wielder_rb.rotation-sword_rotation,wielder_rb.rotation+ sword_rotation, slash_timer / SLASH_DURATION) ;
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

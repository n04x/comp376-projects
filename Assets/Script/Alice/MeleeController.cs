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


    private GameObject sw;
    private Rigidbody2D thisrb;

    private float sword_rotation = 90;
    // Start is called before the first frame update
    void Start()
    {

        alice = GetComponent<PlayerControl>();

        slash_timer = SLASH_DURATION;
        thisrb = GetComponent<Rigidbody2D>();
        sw = null;
    }

  
    void FixedUpdate()
    {
            if (sw == null && Input.GetButtonDown("X") && !alice.isSlashing)
        {
            alice.isSlashing = true;
            thisrb.velocity = Vector2.zero;
            sw = Instantiate(sword, transform.position, transform.rotation);

            //pushes alice forward

            thisrb.velocity = (transform.up* slash_forward_value);
        }


        if(sw != null)
        {
            Slash();
        }
            


    }

    void Slash()
    {
        sw.transform.position = this.transform.position;
        Rigidbody2D sw_rb = sw.GetComponent<Rigidbody2D>();
        if (slash_timer > 0)
        {
            slash_timer -= Time.deltaTime;

            sw_rb.rotation = Mathf.Lerp(thisrb.rotation-sword_rotation,thisrb.rotation+ sword_rotation, slash_timer / SLASH_DURATION) ;
        }
        else
        {
            Destroy(sw);
            sw = null;
            slash_timer = SLASH_DURATION;
            alice.isSlashing = false;
        }
    }


}

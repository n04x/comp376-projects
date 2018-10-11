
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float movementSpeed = 2f;
	 Rigidbody2D rb;

    public bool isDashing = false;
    public bool isInvincible = false;
    public bool shieldOut = false;

    public Vector2 lStickDir;
    public Vector2 rStickDir;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){


	}
	void FixedUpdate () {
        if(!isDashing)
		ProcessInput ();
	}

	void ProcessInput(){

        //Below are left joystick controls
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
         lStickDir = new Vector2(horizontal, vertical);

        if (horizontal != 0 || vertical != 0) { 
        if (lStickDir.magnitude >= 1)
                lStickDir = lStickDir.normalized;
            lStickDir = lStickDir * movementSpeed * Time.deltaTime;
        rb.velocity = lStickDir;
        }

        //Right Stick Controls
        float right_horizontal = Input.GetAxis("RHorizontal");
        float right_vertical = Input.GetAxis("RVertical");
        rStickDir = new Vector2(right_horizontal, right_vertical);
        if (Mathf.Abs(right_horizontal) >= 0.02 || Mathf.Abs(right_vertical) >= 0.02)
        {
            transform.up = (rStickDir);
        }else if(Mathf.Abs(lStickDir.magnitude) >= 0.4f )
        {
            transform.up = lStickDir; //movement without rstick
        }
       


        Debug.Log(rb.velocity.magnitude);
    }


}
